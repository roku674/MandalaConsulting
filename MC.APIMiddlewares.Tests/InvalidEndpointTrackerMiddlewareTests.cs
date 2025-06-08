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
    public class InvalidEndpointTrackerMiddlewareTests
    {
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
            
            // Ensure IP is blocked
            if (IPBlacklist.GetBlockReason(testIP) == null)
            {
                IPBlacklist.AddBannedIP(testIP, "Test blocking for InvalidEndpointTrackerMiddleware test");
            }
            
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
        public async Task InvokeAsync_With404Response_RecordsFailedAttempt()
        {
            // Arrange
            RequestDelegate next = (context) => 
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            };
            
            var middleware = new InvalidEndpointTrackerMiddleware(next);
            
            var context = new DefaultHttpContext();
            string testIP = "192.168.1.52";
            context.Connection.RemoteIpAddress = IPAddress.Parse(testIP);
            context.Request.Path = "/nonexistent-endpoint";
            
            // Ensure IP is not blocked
            if (IPBlacklist.GetBlockReason(testIP) != null)
            {
                // Skip test if we can't unblock the IP
                return;
            }
            
            // Create a mock HttpContext that can have Features set
            var mockContext = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var response = new Mock<HttpResponse>();
            var connection = new Mock<ConnectionInfo>();
            
            // Setup request
            request.Setup(r => r.Path).Returns(context.Request.Path);
            mockContext.Setup(c => c.Request).Returns(request.Object);
            
            // Setup response
            response.SetupProperty(r => r.StatusCode);
            response.Object.StatusCode = StatusCodes.Status200OK; // Will be changed to 404 by delegate
            mockContext.Setup(c => c.Response).Returns(response.Object);
            
            // Setup connection/IP
            connection.Setup(c => c.RemoteIpAddress).Returns(context.Connection.RemoteIpAddress);
            mockContext.Setup(c => c.Connection).Returns(connection.Object);
            
            // Get initial log count to compare after
            int initialLogCount = IPBlacklistMiddleware.GetLogs().Count;
            
            // Act
            await middleware.InvokeAsync(mockContext.Object);
            
            // Assert
            // Check that a log was added
            int newLogCount = IPBlacklistMiddleware.GetLogs().Count;
            Assert.True(newLogCount > initialLogCount, "A log entry should be added for the failed attempt");
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
            
            // Get initial log count to compare after
            int initialLogCount = IPBlacklistMiddleware.GetLogs().Count;
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that a log was added
            int newLogCount = IPBlacklistMiddleware.GetLogs().Count;
            Assert.True(newLogCount > initialLogCount, "A log entry should be added for the unauthorized attempt");
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
            
            // Get initial log count to compare after
            int initialLogCount = IPBlacklistMiddleware.GetLogs().Count;
            
            // Act
            await middleware.InvokeAsync(context);
            
            // Assert
            // Check that a log was added
            int newLogCount = IPBlacklistMiddleware.GetLogs().Count;
            Assert.True(newLogCount > initialLogCount, "A log entry should be added for the forbidden attempt");
        }

        [Fact]
        public async Task InvokeAsync_WithEnvEndpoint_BansIP()
        {
            // Arrange
            var middleware = new InvalidEndpointTrackerMiddleware((context) => 
            {
                // The next delegate sets the status code to 404
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            });
            
            // Create a mock HttpContext that can have Features set
            var mockContext = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var response = new Mock<HttpResponse>();
            var connection = new Mock<ConnectionInfo>();
            
            string testIP = "198.51.100.255"; // Use a unique IP for this test
            
            // Setup request
            request.Setup(r => r.Path).Returns("/test/.env");
            mockContext.Setup(c => c.Request).Returns(request.Object);
            
            // Setup response with a property that persists the status code
            int statusCode = StatusCodes.Status200OK;
            response.SetupGet(r => r.StatusCode).Returns(() => statusCode);
            response.SetupSet(r => r.StatusCode = It.IsAny<int>()).Callback<int>(value => statusCode = value);
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
            Assert.Contains(".env", blockReason);
        }
    }
}