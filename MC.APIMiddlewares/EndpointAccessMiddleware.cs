using MandalaConsulting.Optimization.Memory;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//Copyright © 2024 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.APIMiddleware
{
    public class EndpointAccessMiddleware
    {
        private static readonly ConcurrentDictionary<string, DateTime> _lastAccessed = new ConcurrentDictionary<string, DateTime>();
        private readonly RequestDelegate _next;
        private readonly TimeSpan? _timeout;
        private readonly Timer _timer;
        private readonly GarbageCollection _garbageCollection;
        private readonly bool _cleanMem;

        /// <summary>
        /// Middleware for checking if all endpoints have been idle for a certain amount of time and optionally clean memory.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        /// <param name="timeout">Interval in which you want to check when the last endpoint was hit. If null, no timeout checking will be performed.</param>
        /// <param name="cleanMem">Whether memory cleaning should be performed. Defaults to false.</param>
        public EndpointAccessMiddleware(RequestDelegate next, TimeSpan? timeout, bool cleanMem = false)
        {
            _next = next;
            _timeout = timeout;
            _cleanMem = cleanMem;
            _garbageCollection = new GarbageCollection();

            // Set up a timer to check for garbage collection only if memory cleaning is enabled
            if (_cleanMem)
            {
                _timer = new Timer(CheckForGarbageCollection, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            }
        }

        public async Task Invoke(HttpContext context)
        {
            string path = context.Request.Path.ToString();

            // Update the last accessed time
            _lastAccessed[path] = DateTime.UtcNow;

            await _next(context);
        }

        private void CheckForGarbageCollection(object state)
        {
            if (_timeout.HasValue && AreAllEndpointsIdle())
            {
                _garbageCollection.PerformGarbageCollection(null);
            }
        }

        private bool AreAllEndpointsIdle()
        {
            if (!_timeout.HasValue)
            {
                return false; // No timeout specified, so skip idle check
            }

            DateTime now = DateTime.UtcNow;

            // Check if any endpoint has been accessed within the specified timeout
            foreach (DateTime lastAccessedTime in _lastAccessed.Values)
            {
                if ((now - lastAccessedTime) < _timeout.Value)
                {
                    return false; // At least one endpoint has been hit recently
                }
            }

            return true; // No endpoints have been hit recently
        }

        public static bool HasBeenHitRecently(string path)
        {
            if (_lastAccessed.TryGetValue(path, out DateTime lastAccessed))
            {
                return (DateTime.UtcNow - lastAccessed) < TimeSpan.FromMinutes(5);
            }
            return false; // Endpoint hasn't been hit recently
        }
    }
}
