// Copyright Mandala Consulting, LLC., 2025. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2025-06-08 13:27:40
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;
using MandalaConsulting.APIMiddleware;
using Moq;

namespace MandalaConsulting.APIMiddlewares.Tests
{
    public class EndpointAccessMiddlewareTests
    {
        [Fact]
        public void Constructor_WithTimeoutAndCleanMem_CreatesInstance()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;
            TimeSpan timeout = TimeSpan.FromMinutes(10);
            bool cleanMem = true;

            // Act
            var middleware = new EndpointAccessMiddleware(next, timeout, cleanMem);

            // Assert
            Assert.NotNull(middleware);
        }

        [Fact]
        public void Constructor_WithTimeoutNoCleanMem_CreatesInstance()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;
            TimeSpan timeout = TimeSpan.FromMinutes(10);
            bool cleanMem = false;

            // Act
            var middleware = new EndpointAccessMiddleware(next, timeout, cleanMem);

            // Assert
            Assert.NotNull(middleware);
        }

        [Fact]
        public void Constructor_WithNoTimeout_CreatesInstance()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;
            TimeSpan? timeout = null;

            // Act
            var middleware = new EndpointAccessMiddleware(next, timeout);

            // Assert
            Assert.NotNull(middleware);
        }

        [Fact]
        public async Task Invoke_UpdatesLastAccessedTime()
        {
            // Arrange
            bool nextCalled = false;
            RequestDelegate next = (context) => 
            {
                nextCalled = true;
                return Task.CompletedTask;
            };
            
            var middleware = new EndpointAccessMiddleware(next, TimeSpan.FromMinutes(10));
            
            var context = new DefaultHttpContext();
            context.Request.Path = "/api/test";

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.True(nextCalled, "Next delegate should be called");
            Assert.True(EndpointAccessMiddleware.HasBeenHitRecently("/api/test"), "The endpoint should be marked as recently accessed");
        }

        [Fact]
        public void HasBeenHitRecently_ReturnsFalse_ForUnknownPath()
        {
            // Arrange
            string unknownPath = "/api/unknown-" + Guid.NewGuid();
            
            // Act
            bool result = EndpointAccessMiddleware.HasBeenHitRecently(unknownPath);
            
            // Assert
            Assert.False(result, "Unknown path should not be marked as recently accessed");
        }

        [Fact]
        public async Task Invoke_CallsNextMiddleware()
        {
            // Arrange
            var nextCalled = false;
            RequestDelegate next = (context) => 
            {
                nextCalled = true;
                return Task.CompletedTask;
            };
            
            var middleware = new EndpointAccessMiddleware(next, TimeSpan.FromMinutes(10));
            var context = new DefaultHttpContext();
            
            // Act
            await middleware.Invoke(context);
            
            // Assert
            Assert.True(nextCalled, "Next middleware should be called");
        }

        [Fact]
        public async Task Invoke_SamePathTwice_UpdatesLastAccessTime()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;
            var middleware = new EndpointAccessMiddleware(next, TimeSpan.FromMinutes(10));
            
            var path = "/api/test-" + Guid.NewGuid();
            var context = new DefaultHttpContext();
            context.Request.Path = path;
            
            // Act - First request
            await middleware.Invoke(context);
            bool firstResult = EndpointAccessMiddleware.HasBeenHitRecently(path);
            
            // Wait a tiny bit to ensure time difference
            await Task.Delay(10);
            
            // Act - Second request
            await middleware.Invoke(context);
            
            // Assert
            Assert.True(firstResult, "Path should be marked as recently accessed after first hit");
            Assert.True(EndpointAccessMiddleware.HasBeenHitRecently(path), "Path should be marked as recently accessed after second hit");
        }

        [Fact]
        public async Task MultiplePaths_TrackedIndependently()
        {
            // Arrange
            RequestDelegate next = (context) => Task.CompletedTask;
            var middleware = new EndpointAccessMiddleware(next, TimeSpan.FromMinutes(10));
            
            var path1 = "/api/test1-" + Guid.NewGuid();
            var path2 = "/api/test2-" + Guid.NewGuid();
            
            var context1 = new DefaultHttpContext();
            context1.Request.Path = path1;
            
            var context2 = new DefaultHttpContext();
            context2.Request.Path = path2;
            
            // Act
            await middleware.Invoke(context1);
            await middleware.Invoke(context2);
            
            // Assert
            Assert.True(EndpointAccessMiddleware.HasBeenHitRecently(path1), "Path1 should be marked as recently accessed");
            Assert.True(EndpointAccessMiddleware.HasBeenHitRecently(path2), "Path2 should be marked as recently accessed");
        }
    }
}
