// Copyright Mandala Consulting, LLC., 2025. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2025-06-08 13:27:40
using Xunit;
using MandalaConsulting.APIMiddleware.Objects;
using MandalaConsulting.APIMiddleware;
using System;

namespace MandalaConsulting.APIMiddlewares.Tests.Objects
{
    [Collection("Sequential")]
    public class IPBlacklistTests : IDisposable
    {
        public IPBlacklistTests()
        {
            // Clear state before each test
            IPBlacklist.ClearBlacklist();
            IPBlacklistMiddleware.ClearLogs();
        }

        public void Dispose()
        {
            // Clear state after each test
            IPBlacklist.ClearBlacklist();
            IPBlacklistMiddleware.ClearLogs();
        }

        [Fact]
        public void AddBannedIP_AddsIPToBlacklist()
        {
            // Arrange
            string ip = "192.168.1.1";
            string reason = "Test reason";
            
            bool eventRaised = false;
            EventHandler<BannedIP> handler = (sender, e) => 
            {
                eventRaised = true;
                Assert.Equal(ip, e.ipv4);
                Assert.Equal(reason, e.reason);
            };
            IPBlacklist.IPBanned += handler;
            
            // Act
            IPBlacklist.AddBannedIP(ip, reason);
            
            // Assert
            Assert.True(eventRaised, "IPBanned event should be raised");
            Assert.Equal(reason, IPBlacklist.GetBlockReason(ip));
            Assert.True(IPBlacklist.IsIPBlocked(ip));
            
            // Cleanup
            IPBlacklist.IPBanned -= handler;
        }
        
        [Fact]
        public void AddBannedIP_WhenIPAlreadyExists_DoesNotRaiseEvent()
        {
            // Arrange
            string ip = "192.168.1.2";
            string reason1 = "Test reason 1";
            string reason2 = "Test reason 2";
            
            // Add IP first time
            IPBlacklist.AddBannedIP(ip, reason1);
            
            bool eventRaised = false;
            EventHandler<BannedIP> handler = (sender, e) => eventRaised = true;
            IPBlacklist.IPBanned += handler;
            
            // Act - try to add again
            IPBlacklist.AddBannedIP(ip, reason2);
            
            // Assert
            Assert.False(eventRaised, "IPBanned event should not be raised for already banned IP");
            // The reason should not change
            Assert.Equal(reason1, IPBlacklist.GetBlockReason(ip));
            
            // Cleanup
            IPBlacklist.IPBanned -= handler;
        }
        
        [Fact]
        public void GetBlockReason_ForNonBlacklistedIP_ReturnsNull()
        {
            // Arrange
            string ip = "192.168.1.100"; // Assuming this IP is not blacklisted
            
            // Act
            string reason = IPBlacklist.GetBlockReason(ip);
            
            // Assert
            Assert.Null(reason);
        }
        
        [Fact]
        public void IsIPBlocked_ForNonBlacklistedIP_ReturnsFalse()
        {
            // Arrange
            string ip = "192.168.1.101"; // Assuming this IP is not blacklisted
            
            // Act
            bool isBlocked = IPBlacklist.IsIPBlocked(ip);
            
            // Assert
            Assert.False(isBlocked);
        }
        
        [Fact]
        public void AddBannedIP_WithMultipleIPs_AllAreBlacklisted()
        {
            // Arrange
            string ip1 = "192.168.1.11";
            string reason1 = "Test reason 1";
            
            string ip2 = "192.168.1.12";
            string reason2 = "Test reason 2";
            
            // Act
            IPBlacklist.AddBannedIP(ip1, reason1);
            IPBlacklist.AddBannedIP(ip2, reason2);
            
            // Assert
            Assert.Equal(reason1, IPBlacklist.GetBlockReason(ip1));
            Assert.Equal(reason2, IPBlacklist.GetBlockReason(ip2));
            Assert.True(IPBlacklist.IsIPBlocked(ip1));
            Assert.True(IPBlacklist.IsIPBlocked(ip2));
        }
    }
}
