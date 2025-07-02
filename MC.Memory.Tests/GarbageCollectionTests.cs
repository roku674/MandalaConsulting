// Copyright © Mandala Consulting, LLC., 2025. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2025-06-08 13:27:40
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
using System;
using Xunit;
using MandalaConsulting.Optimization.Memory;

namespace MandalaConsulting.Memory.Tests
{
    public class GarbageCollectionTests
    {
        [Fact]
        public void Constructor_CreatesInstance()
        {
            // Act
            var gc = new GarbageCollection();

            // Assert
            Assert.NotNull(gc);
        }

        [Fact]
        public void PerformGarbageCollection_DoesNotThrowException()
        {
            // Arrange
            var gc = new GarbageCollection();
            object state = null;

            // Act & Assert
            var exception = Record.Exception(() => gc.PerformGarbageCollection(state));
            Assert.Null(exception);
        }

        [Fact]
        public void PerformGarbageCollection_WithNonNullState_DoesNotThrowException()
        {
            // Arrange
            var gc = new GarbageCollection();
            object state = new object();

            // Act & Assert
            var exception = Record.Exception(() => gc.PerformGarbageCollection(state));
            Assert.Null(exception);
        }

        [Fact]
        public void PerformGarbageCollection_CanBeCalledMultipleTimes()
        {
            // Arrange
            var gc = new GarbageCollection();
            object state = null;

            // Act & Assert
            var exception1 = Record.Exception(() => gc.PerformGarbageCollection(state));
            var exception2 = Record.Exception(() => gc.PerformGarbageCollection(state));
            var exception3 = Record.Exception(() => gc.PerformGarbageCollection(state));

            Assert.Null(exception1);
            Assert.Null(exception2);
            Assert.Null(exception3);
        }

        [Fact]
        public void GarbageCollection_MemoryTest()
        {
            // Arrange
            var gc = new GarbageCollection();
            object state = null;
            
            // Allocate some memory that will be garbage collected
            var memoryHog = new byte[1024 * 1024]; // 1MB
            
            // Act
            long memoryBefore = GC.GetTotalMemory(false);
            gc.PerformGarbageCollection(state);
            memoryHog = null; // Make the memory available for collection
            gc.PerformGarbageCollection(state);
            long memoryAfter = GC.GetTotalMemory(true);
            
            // Assert
            // We're not strictly asserting an exact value because memory management
            // varies by runtime, but we can assert it didn't increase
            Assert.True(memoryAfter <= memoryBefore);
        }
    }
}
