using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MandalaConsulting.Optimization.Logging;
using MongoDB.Driver;
using Xunit;
using Moq;

namespace MandalaConsulting.Repository.Mongo.Tests
{
    public class MongoHelperTests
    {
        private class TestDocument
        {
            public string _id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            // Act
            var mongoHelper = new MongoHelper();

            // Assert
            Assert.NotNull(mongoHelper);
            Assert.Null(mongoHelper.database);
        }

        [Fact]
        public void ParameterizedConstructor_InitializesDatabase()
        {
            // Arrange - This test requires a real MongoDB instance, so we'll skip actual validation
            string dbName = "test-db";
            string connectionString = "mongodb://localhost:27017";
            
            // Act
            var mongoHelper = new MongoHelper(dbName, connectionString);
            
            // Assert
            Assert.NotNull(mongoHelper);
            Assert.NotNull(mongoHelper.database);
        }

        [Fact]
        public void ConnectionStringBuilder_FormatsConnectionStringCorrectly()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            string cluster = "testcluster";
            string region = "testregion";
            
            // Act
            string connectionString = MongoHelper.ConnectionStringBuilder(username, password, cluster, region);
            
            // Assert
            string expected = $"mongodb+srv://{username}:{Uri.EscapeDataString(password)}@{cluster}.{region}.mongodb.net/?retryWrites=true&w=majority";
            Assert.Equal(expected, connectionString);
        }

        [Fact]
        public void ConnectionStringBuilder_EncodesPasswordWithSpecialCharacters()
        {
            // Arrange
            string username = "testuser";
            string password = "test@pass&";
            string cluster = "testcluster";
            string region = "testregion";
            
            // Act
            string connectionString = MongoHelper.ConnectionStringBuilder(username, password, cluster, region);
            
            // Assert
            string encodedPassword = Uri.EscapeDataString(password);
            string expected = $"mongodb+srv://{username}:{encodedPassword}@{cluster}.{region}.mongodb.net/?retryWrites=true&w=majority";
            Assert.Equal(expected, connectionString);
        }

        [Fact]
        public void GetIdFromObj_ReturnsIdProperty()
        {
            // Arrange
            var testDoc = new TestDocument { _id = "test-id", Name = "Test", Age = 30 };
            
            // Act
            object id = MongoHelper.GetIdFromObj(testDoc);
            
            // Assert
            Assert.Equal("test-id", id);
        }

        [Fact]
        public void GetIdFromObj_ReturnsNull_WhenNoIdProperty()
        {
            // Arrange
            var objWithNoId = new { Name = "Test", Age = 30 };
            
            // Act
            object id = MongoHelper.GetIdFromObj(objWithNoId);
            
            // Assert
            Assert.Null(id);
        }

        [Fact]
        public void GetIdFromObjAsString_ReturnsIdAsString()
        {
            // Arrange
            var testDoc = new TestDocument { _id = "test-id", Name = "Test", Age = 30 };
            
            // Act
            string id = MongoHelper.GetIdFromObjAsString(testDoc);
            
            // Assert
            Assert.Equal("test-id", id);
        }

        [Fact]
        public void GetIdFromObjAsString_ReturnsNull_WhenNoIdProperty()
        {
            // Arrange
            var objWithNoId = new { Name = "Test", Age = 30 };
            
            // Act
            string id = MongoHelper.GetIdFromObjAsString(objWithNoId);
            
            // Assert
            Assert.Null(id);
        }

        [Fact]
        public void AddLog_AddsLogToQueue_AndTriggersEvent()
        {
            // Arrange
            MongoHelper.ClearLogs();
            bool eventRaised = false;
            
            EventHandler<LogMessageEventArgs> handler = (sender, e) => eventRaised = true;
            MongoHelper.LogAdded += handler;
            
            // Use reflection to call the protected method
            var mongoHelper = new MongoHelper();
            var methodInfo = typeof(MongoHelper).GetMethod("AddLog", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var parameters = new object[] { LogMessage.Information("Test log message") };
            
            // Act
            methodInfo.Invoke(null, parameters);
            
            // Assert
            var logs = MongoHelper.GetLogs();
            Assert.Single(logs);
            Assert.True(eventRaised, "LogAdded event should be triggered");
            
            // Cleanup
            MongoHelper.LogAdded -= handler;
        }

        [Fact]
        public void ClearLogs_EmptiesLogQueue_AndTriggersEvent()
        {
            // Arrange
            // Add some logs (using reflection to call the protected method)
            var methodInfo = typeof(MongoHelper).GetMethod("AddLog", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            methodInfo.Invoke(null, new object[] { LogMessage.Information("Test log message 1") });
            methodInfo.Invoke(null, new object[] { LogMessage.Information("Test log message 2") });
            
            bool eventRaised = false;
            EventHandler handler = (sender, e) => eventRaised = true;
            
            MongoHelper.LogCleared += handler;
            
            // Act
            MongoHelper.ClearLogs();
            
            // Assert
            var logs = MongoHelper.GetLogs();
            Assert.Empty(logs);
            Assert.True(eventRaised, "LogCleared event should be triggered");
            
            // Cleanup
            MongoHelper.LogCleared -= handler;
        }

        [Fact]
        public void GetLogs_ReturnsAllLogs()
        {
            // Arrange
            MongoHelper.ClearLogs();
            
            // Add some logs (using reflection to call the protected method)
            var methodInfo = typeof(MongoHelper).GetMethod("AddLog", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            var log1 = LogMessage.Information("Test log message 1");
            var log2 = LogMessage.Information("Test log message 2");
            var log3 = LogMessage.Information("Test log message 3");
            
            methodInfo.Invoke(null, new object[] { log1 });
            methodInfo.Invoke(null, new object[] { log2 });
            methodInfo.Invoke(null, new object[] { log3 });
            
            // Act
            var logs = MongoHelper.GetLogs();
            
            // Assert
            Assert.Equal(3, logs.Count);
        }
    }
}