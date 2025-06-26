using System;
using System.Threading.Tasks;
using Xunit;
using MandalaConsulting.Optimization.Logging;

namespace MandalaConsulting.Logging.Tests
{
    public class LogMessageTests
    {
        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            // Act
            var logMessage = new LogMessage();

            // Assert
            Assert.NotNull(logMessage);
        }

        [Fact]
        public void Constructor_WithMessageType_SetsProperties()
        {
            // Arrange
            var messageType = MessageType.Informational;
            var messageText = "Test message";
            
            // Act
            var logMessage = new LogMessage(messageType, messageText);

            // Assert
            Assert.Equal(messageType, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
            Assert.NotNull(logMessage.localOperationName);
            Assert.NotEqual(default(DateTime), logMessage.timeStamp);
            Assert.Equal(LogMessage.MessageSourceSetter, logMessage.messageSource);
        }

        [Fact]
        public void Constructor_WithIdAndMessageType_SetsProperties()
        {
            // Arrange
            int id = 42;
            var messageType = MessageType.Warning;
            var messageText = "Test warning message";
            
            // Act
            var logMessage = new LogMessage(id, messageType, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(messageType, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
            Assert.NotNull(logMessage.localOperationName);
            Assert.NotEqual(default(DateTime), logMessage.timeStamp);
            Assert.Equal(LogMessage.MessageSourceSetter, logMessage.messageSource);
        }

        [Fact]
        public void Celebrate_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Celebration message";
            
            // Act
            var logMessage = LogMessage.Celebrate(messageText);

            // Assert
            Assert.Equal(MessageType.Celebrate, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Celebrate_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 1;
            var messageText = "Celebration message with id";
            
            // Act
            var logMessage = LogMessage.Celebrate(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Celebrate, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Critical_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Critical message";
            
            // Act
            var logMessage = LogMessage.Critical(messageText);

            // Assert
            Assert.Equal(MessageType.Critical, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Critical_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 2;
            var messageText = "Critical message with id";
            
            // Act
            var logMessage = LogMessage.Critical(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Critical, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Error_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Error message";
            
            // Act
            var logMessage = LogMessage.Error(messageText);

            // Assert
            Assert.Equal(MessageType.Error, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Error_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 3;
            var messageText = "Error message with id";
            
            // Act
            var logMessage = LogMessage.Error(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Error, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Informational_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Informational message";
            
            // Act
            var logMessage = LogMessage.Informational(messageText);

            // Assert
            Assert.Equal(MessageType.Informational, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Informational_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 4;
            var messageText = "Informational message with id";
            
            // Act
            var logMessage = LogMessage.Informational(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Informational, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Message_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Regular message";
            
            // Act
            var logMessage = LogMessage.Message(messageText);

            // Assert
            Assert.Equal(MessageType.Message, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Message_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 5;
            var messageText = "Regular message with id";
            
            // Act
            var logMessage = LogMessage.Message(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Message, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Success_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Success message";
            
            // Act
            var logMessage = LogMessage.Success(messageText);

            // Assert
            Assert.Equal(MessageType.Success, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Success_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 6;
            var messageText = "Success message with id";
            
            // Act
            var logMessage = LogMessage.Success(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Success, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Warning_CreatesMessage_WithCorrectType()
        {
            // Arrange
            var messageText = "Warning message";
            
            // Act
            var logMessage = LogMessage.Warning(messageText);

            // Assert
            Assert.Equal(MessageType.Warning, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void Warning_WithId_CreatesMessage_WithCorrectTypeAndId()
        {
            // Arrange
            int id = 7;
            var messageText = "Warning message with id";
            
            // Act
            var logMessage = LogMessage.Warning(id, messageText);

            // Assert
            Assert.Equal(id, logMessage.id);
            Assert.Equal(MessageType.Warning, logMessage.messageType);
            Assert.Equal(messageText, logMessage.message);
        }

        [Fact]
        public void LogMessageEventArgs_Constructor_SetsLogProperty()
        {
            // Arrange
            var logMessage = new LogMessage(MessageType.Informational, "Test message");
            
            // Act
            var args = new LogMessageEventArgs(logMessage);
            
            // Assert
            Assert.Same(logMessage, args.log);
        }

        [Fact]
        public void MessageSourceSetter_CanBeModified()
        {
            // Arrange
            string originalValue = LogMessage.MessageSourceSetter;
            string newValue = "TestApplication";
            
            // Act
            LogMessage.MessageSourceSetter = newValue;
            var logMessage = new LogMessage(MessageType.Informational, "Test message");
            
            // Cleanup (restore original value)
            LogMessage.MessageSourceSetter = originalValue;
            
            // Assert
            Assert.Equal(newValue, logMessage.messageSource);
        }

        [Fact]
        public async Task AsyncMethod_LogsCorrectOperationName()
        {
            // Act
            LogMessage logMessage = null;
            await TestAsyncMethodAsync(() => 
            {
                logMessage = new LogMessage(MessageType.Informational, "Test async logging");
            });

            // Assert
            Assert.NotNull(logMessage);
            // Log the actual operation name for debugging
            Console.WriteLine($"Actual operation name: {logMessage.localOperationName}");
            Assert.NotNull(logMessage.localOperationName);
            Assert.DoesNotContain("MoveNext", logMessage.localOperationName);
        }

        private async Task TestAsyncMethodAsync(Action createLog)
        {
            await Task.Delay(1); // Simulate async work
            createLog();
        }

        [Fact]
        public void AsyncMethodDirectCall_LogsCorrectOperationName()
        {
            // Act
            LogMessage logMessage = null;
            Task.Run(async () =>
            {
                await Task.Delay(1);
                logMessage = new LogMessage(MessageType.Informational, "Test from async context");
            }).Wait();

            // Assert
            Assert.NotNull(logMessage);
            // Since this is from an anonymous async lambda, it should still capture something meaningful
            Assert.NotNull(logMessage.localOperationName);
            Assert.DoesNotContain("MoveNext", logMessage.localOperationName);
        }
    }
}