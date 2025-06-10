using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Xunit;
using MandalaConsulting.APIMiddleware;
using MandalaConsulting.APIMiddleware.Objects;
using Moq;
using System.Net;

namespace MandalaConsulting.APIMiddlewares.Tests
{
    [Collection("Sequential")]
    public class InvalidEndpointTrackerMiddlewareTests : IDisposable
    {
        public InvalidEndpointTrackerMiddlewareTests()
        {
            // Clear state before each test
            IPBlacklist.ClearBlacklist();
            IPBlacklistMiddleware.ClearLogs();
            InvalidEndpointTrackerMiddleware.ClearFailedAttempts();
        }

        public void Dispose()
        {
            // Clear state after each test
            IPBlacklist.ClearBlacklist();
            IPBlacklistMiddleware.ClearLogs();
            InvalidEndpointTrackerMiddleware.ClearFailedAttempts();
        }
        [Fact]
        public void Constructor_CreatesInstance()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;

            // Act
            var middleware = new InvalidEndpointTrackerMiddleware(next);

            // Assert
            Assert.NotNull(middleware);
        }

        [Fact]
        public async Task InvokeAsync_WithBlockedIP_Returns403()
        {
            // Arrange
            bool nextCalled = false;
            RequestDelegate next = (context) => 
            {
                nextCalled = true;
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            string testIP = "192.168.1.50";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            
            // Add IP to blacklist
            IPBlacklist.AddBannedIP(testIP, "Test blocking for InvalidEndpointTrackerMiddleware test");
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
            Assert.False(nextCalled, "Next delegate should not be called for blocked IP");
        }

        [Fact]
        public async Task InvokeAsync_WithNonBlockedIP_CallsNext()
        {
            // Arrange
            bool nextCalled = false;
            RequestDelegate next = (context) => 
            {
                nextCalled = true;
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            string testIP = "192.168.1.51";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            Assert.True(nextCalled, "Next delegate should be called for non-blocked IP");
        }

        [Fact]
        public async Task InvokeAsync_WithNonExistentEndpoint_RecordsFailedAttempt()
        {
            // Arrange
            RequestDelegate next = (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                // Don't write any content - simulating a truly non-existent endpoint
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream(); // Need a writable stream
            string testIP = "192.168.1.52";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/nonexistent-endpoint";
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Get initial log count to compare after
            int initialLogCount = IPBlacklistMiddleware.GetLogs().Count;
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that a log was added
            int newLogCount = IPBlacklistMiddleware.GetLogs().Count;
            Assert.True(newLogCount > initialLogCount, "A log entry should be added for the failed attempt on non-existent endpoint");
        }

        [Fact]
        public async Task InvokeAsync_WithExistingEndpointReturning404_DoesNotRecordFailedAttempt()
        {
            // Arrange
            RequestDelegate next = async (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                // Write some content to simulate an endpoint that executed but returned 404
                await context.Response.WriteAsync("{\"error\": \"User not found\"}");
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream(); // Need a writable stream
            string testIP = "192.168.1.56";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/api/users/999"; // Existing endpoint but user not found
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Get initial log count to compare after
            int initialLogCount = IPBlacklistMiddleware.GetLogs().Count;
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that NO log was added (legitimate 404 from existing endpoint that wrote content)
            int newLogCount = IPBlacklistMiddleware.GetLogs().Count;
            Assert.Equal(initialLogCount, newLogCount);
        }

        [Fact]
        public async Task InvokeAsync_WithRootPath404_DoesNotRecordFailedAttempt()
        {
            // Arrange
            RequestDelegate next = (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                // Don't write any content - simulating no endpoint at root
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream(); // Need a writable stream
            string testIP = "192.168.1.57";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/"; // Root path
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Get initial log count to compare after
            int initialLogCount = IPBlacklistMiddleware.GetLogs().Count;
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that NO log was added (root path is excluded from tracking)
            int newLogCount = IPBlacklistMiddleware.GetLogs().Count;
            Assert.Equal(initialLogCount, newLogCount);
        }

        [Fact]
        public async Task InvokeAsync_With401Response_RecordsFailedAttempt()
        {
            // Arrange
            RequestDelegate next = (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            string testIP = "192.168.1.53";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/unauthorized-endpoint";
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Clear logs before test to ensure clean state
            IPBlacklistMiddleware.ClearLogs();
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that a log was added
            var logs = IPBlacklistMiddleware.GetLogs();
            Assert.NotEmpty(logs);
            var lastLog = logs[logs.Count - 1];
            Assert.Contains("unauthorized", lastLog.message.ToLower());
        }

        [Fact]
        public async Task InvokeAsync_With403Response_RecordsFailedAttempt()
        {
            // Arrange
            RequestDelegate next = (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            string testIP = "192.168.1.54";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/forbidden-endpoint";
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Clear logs before test to ensure clean state
            IPBlacklistMiddleware.ClearLogs();
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that a log was added
            var logs = IPBlacklistMiddleware.GetLogs();
            Assert.NotEmpty(logs);
            var lastLog = logs[logs.Count - 1];
            Assert.Contains("forbidden", lastLog.message.ToLower());
        }

        [Fact]
        public async Task InvokeAsync_WithEnvEndpoint_BansIP()
        {
            // Arrange
            RequestDelegate next = (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream(); // Need a writable stream
            string testIP = "192.168.1.55";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/test/.env";
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Important: Don't write any content - simulating a truly non-existent route
            // which is typically the case for .env files
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that the IP was banned
            string blockReason = IPBlacklist.GetBlockReason(testIP);
            Assert.NotNull(blockReason);
            Assert.Contains(".env", blockReason);
        }
    }
}