// Copyright Mandala Consulting, LLC., 2025. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2025-06-08 13:27:40
using Xunit;
using MandalaConsulting.APIMiddleware;
using System.Collections.Generic;

namespace MandalaConsulting.APIMiddlewares.Tests
{
    public class AttemptInfoTests
    {
        [Fact]
        public void Constructor_InitializedWithZeroCount()
        {
            // Act
            var attemptInfo = new AttemptInfo();
            
            // Assert
            Assert.Equal(0, attemptInfo.Count);
            Assert.NotNull(attemptInfo.Paths);
            Assert.Empty(attemptInfo.Paths);
        }

        [Fact]
        public void Paths_AddingItems_UpdatesCollection()
        {
            // Arrange
            var attemptInfo = new AttemptInfo();
            string path1 = "/path1";
            string path2 = "/path2";
            
            // Act
            attemptInfo.Paths.Add(path1);
            attemptInfo.Paths.Add(path2);
            
            // Assert
            Assert.Equal(2, attemptInfo.Paths.Count);
            Assert.Contains(path1, attemptInfo.Paths);
            Assert.Contains(path2, attemptInfo.Paths);
        }

        [Fact]
        public void Paths_AddingDuplicateItem_DoesNotIncreaseCount()
        {
            // Arrange
            var attemptInfo = new AttemptInfo();
            string path = "/path";
            
            // Act
            attemptInfo.Paths.Add(path);
            attemptInfo.Paths.Add(path); // Add the same path again
            
            // Assert
            Assert.Equal(1, attemptInfo.Paths.Count);
            Assert.Contains(path, attemptInfo.Paths);
        }

        [Fact]
        public void Count_CanBeIncremented()
        {
            // Arrange
            var attemptInfo = new AttemptInfo();
            
            // Act
            attemptInfo.Count++;
            attemptInfo.Count++;
            
            // Assert
            Assert.Equal(2, attemptInfo.Count);
        }

        [Fact]
        public void Count_CanBeSet()
        {
            // Arrange
            var attemptInfo = new AttemptInfo();
            
            // Act
            attemptInfo.Count = 5;
            
            // Assert
            Assert.Equal(5, attemptInfo.Count);
        }
    }
}
