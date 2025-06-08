//Copyright © 2024 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using MandalaConsulting.APIMiddleware.Objects;
using MandalaConsulting.APIMiddleware.Utility;
using MandalaConsulting.Optimization.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandalaConsulting.APIMiddleware
{
    public class IPBlacklistMiddleware
    {
        private static readonly ConcurrentQueue<LogMessage> middlewareLogs = new ConcurrentQueue<LogMessage>();
        private readonly RequestDelegate _next;

        public IPBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public static event EventHandler<LogMessageEventArgs> LogAdded;

        public static event EventHandler LogCleared;

        public static void AddLog(LogMessage logMessage)
        {
            middlewareLogs.Enqueue(logMessage);
            LogAdded?.Invoke(null, new LogMessageEventArgs(logMessage));
        }

        public static void ClearLogs()
        {
            LogCleared?.Invoke(null, EventArgs.Empty);
            middlewareLogs.Clear();
            // Clear all event handlers for test isolation
            LogAdded = null;
            LogCleared = null;
        }

        public static IList<LogMessage> GetLogs()
        {
            return middlewareLogs.ToList();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string clientIP = APIUtility.GetClientPublicIPAddress(context);

            if (IPBlacklist.IsIPBlocked(clientIP))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                AddLog(LogMessage.Message($"{clientIP} is blocked.")); // Example log addition
                return;
            }

            await _next(context);
        }
    }
}