using Xunit;
using MandalaConsulting.APIMiddleware.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MandalaConsulting.APIMiddlewares.Tests.Utility
{
    public class APIUtilityTests
    {
        [Fact]
        public void GetClientPublicIPAddress_FromHttpContext_WithXForwardedFor_ReturnsCorrectIP()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            string expectedIP = "203.0.113.1";
            
            httpContext.Request.Headers["X-Forwarded-For"] = expectedIP;
            
            // Act
            string resultIP = APIUtility.GetClientPublicIPAddress(httpContext);
            
            // Assert
            Assert.Equal(expectedIP, resultIP);
        }

        [Fact]
        public void GetClientPublicIPAddress_FromHttpContext_WithoutXForwardedFor_UsesRemoteIpAddress()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            string expectedIP = "198.51.100.1";
            
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse(expectedIP);
            
            // Act
            string resultIP = APIUtility.GetClientPublicIPAddress(httpContext);
            
            // Assert
            Assert.Equal(expectedIP, resultIP);
        }

        [Fact]
        public void GetClientPublicIPAddress_FromHttpContext_WithLocalhost_ReturnsDefaultIP()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse("::1");
            
            // Act
            string resultIP = APIUtility.GetClientPublicIPAddress(httpContext);
            
            // Assert
            Assert.Equal("198.51.100.255", resultIP);
        }

        [Fact]
        public void GetClientPublicIPAddress_FromActionContext_WithXForwardedFor_ReturnsCorrectIP()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            string expectedIP = "203.0.113.2";
            
            httpContext.Request.Headers["X-Forwarded-For"] = expectedIP;
            
            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor()
            );
            
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object
            );
            
            // Act
            string resultIP = APIUtility.GetClientPublicIPAddress(actionExecutingContext);
            
            // Assert
            Assert.Equal(expectedIP, resultIP);
        }

        [Fact]
        public void GetClientPublicIPAddress_FromActionContext_WithoutXForwardedFor_UsesRemoteIpAddress()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            string expectedIP = "198.51.100.2";
            
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse(expectedIP);
            
            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor()
            );
            
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object
            );
            
            // Act
            string resultIP = APIUtility.GetClientPublicIPAddress(actionExecutingContext);
            
            // Assert
            Assert.Equal(expectedIP, resultIP);
        }

        [Fact]
        public void GetClientPublicIPAddress_FromActionContext_WithLocalhost_ReturnsDefaultIP()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse("::1");
            
            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor()
            );
            
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object
            );
            
            // Act
            string resultIP = APIUtility.GetClientPublicIPAddress(actionExecutingContext);
            
            // Assert
            Assert.Equal("198.51.100.255", resultIP);
        }
    }
}