using System;

// Copyright © 2023 Mandala Consulting, LLC All rights reserved.
// Created by Alexander Fields

namespace MandalaConsulting.Optimization.Memory
{
    public class GarbageCollection
    {
        private bool _disposed;

        public GarbageCollection()
        {
        }

        ~GarbageCollection()
        {
            GC.SuppressFinalize(this);
        }

        public void PerformGarbageCollection(object state)
        {
            // Perform garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Attempt to return memory to the OS
            ReturnUnusedMemoryToOS();
            //Log the finish?
        }

        private void ReturnUnusedMemoryToOS()
        {
            // Attempt to return unused memory to the OS
            // This can fail if memory is still in use
            long memorySize = GC.GetTotalMemory(false);

            // Check if it's feasible to enter a no GC region
            if (memorySize > 0)
            {
                try
                {
                    // Attempt to allocate a no GC region, if successful return memory
                    if (GC.TryStartNoGCRegion(memorySize))
                    {
                        // End the no GC region and force the GC to return memory
                        GC.EndNoGCRegion();
                    }
                }
                catch (InvalidOperationException)
                {
                    //Log the failure
                }
            }
        }
    }
}