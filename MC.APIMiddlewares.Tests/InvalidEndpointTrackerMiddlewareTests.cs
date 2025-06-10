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
            
            // Create a mock HttpContext that can have Features set
            var mockContext = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var response = new Mock<HttpResponse>();
            var connection = new Mock<ConnectionInfo>();
            
            string testIP = "192.168.1.55";
            
            // Setup request
            var headers = new HeaderDictionary();
            request.Setup(r => r.Headers).Returns(headers);
            request.Setup(r => r.Path).Returns("/test/.env");
            mockContext.Setup(c => c.Request).Returns(request.Object);
            
            // Setup response
            response.SetupProperty(r => r.StatusCode);
            response.Object.StatusCode = StatusCodes.Status200OK; 
            mockContext.Setup(c => c.Response).Returns(response.Object);
            
            // Setup connection/IP
            connection.Setup(c => c.RemoteIpAddress).Returns(IPAddress.Parse(testIP));
            mockContext.Setup(c => c.Connection).Returns(connection.Object);
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Act
            await middleware.InvokeAsync(mockContext.Object);
            
            // Assert
            // Check that the IP was banned
            string blockReason = IPBlacklist.GetBlockReason(testIP);
            Assert.NotNull(blockReason);
        }
    }
}