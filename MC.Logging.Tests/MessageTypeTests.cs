using Xunit;
using MandalaConsulting.Optimization.Logging;

namespace MandalaConsulting.Logging.Tests
{
    public class MessageTypeTests
    {
        [Fact]
        public void MessageType_HasCorrectValues()
        {
            // Assert
            Assert.Equal(0, (int)MessageType.Error);
            Assert.Equal(1, (int)MessageType.Warning);
            Assert.Equal(2, (int)MessageType.Success);
            Assert.Equal(3, (int)MessageType.Informational);
            Assert.Equal(4, (int)MessageType.Message);
            Assert.Equal(5, (int)MessageType.Critical);
            Assert.Equal(6, (int)MessageType.Celebrate);
        }
    }
}