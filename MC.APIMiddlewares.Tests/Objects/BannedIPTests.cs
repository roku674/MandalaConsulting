using Xunit;
using MandalaConsulting.APIMiddleware.Objects;

namespace MandalaConsulting.APIMiddlewares.Tests.Objects
{
    public class BannedIPTests
    {
        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            // Act
            var bannedIP = new BannedIP();

            // Assert
            Assert.NotNull(bannedIP);
            Assert.Null(bannedIP._id);
            Assert.Null(bannedIP.ipv4);
            Assert.Null(bannedIP.ipv6);
            Assert.Null(bannedIP.reason);
        }

        [Fact]
        public void Constructor_WithIPAddresses_SetsIPAddressesOnly()
        {
            // Arrange
            string ipv4 = "192.168.1.1";
            string ipv6 = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";

            // Act
            var bannedIP = new BannedIP(ipv4, ipv6);

            // Assert
            Assert.Equal(ipv4, bannedIP.ipv4);
            Assert.Equal(ipv6, bannedIP.ipv6);
            Assert.Null(bannedIP._id);
            Assert.Null(bannedIP.reason);
        }

        [Fact]
        public void Constructor_WithAllParameters_SetsAllProperties()
        {
            // Arrange
            string id = "test-id";
            string ipv4 = "192.168.1.1";
            string ipv6 = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
            string reason = "Test reason";

            // Act
            var bannedIP = new BannedIP(id, ipv4, ipv6, reason);

            // Assert
            Assert.Equal(id, bannedIP._id);
            Assert.Equal(ipv4, bannedIP.ipv4);
            Assert.Equal(ipv6, bannedIP.ipv6);
            Assert.Equal(reason, bannedIP.reason);
        }

        [Fact]
        public void Properties_CanBeModified()
        {
            // Arrange
            var bannedIP = new BannedIP();
            string id = "test-id";
            string ipv4 = "192.168.1.1";
            string ipv6 = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
            string reason = "Test reason";

            // Act
            bannedIP._id = id;
            bannedIP.ipv4 = ipv4;
            bannedIP.ipv6 = ipv6;
            bannedIP.reason = reason;

            // Assert
            Assert.Equal(id, bannedIP._id);
            Assert.Equal(ipv4, bannedIP.ipv4);
            Assert.Equal(ipv6, bannedIP.ipv6);
            Assert.Equal(reason, bannedIP.reason);
        }
    }
}