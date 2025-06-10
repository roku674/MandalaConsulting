﻿//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using MandalaConsulting.APIMiddleware.Objects;
using MandalaConsulting.APIMiddleware.Utility;
using MandalaConsulting.Optimization.Logging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MandalaConsulting.APIMiddleware
{
    public class AttemptInfo
    {
        public int Count { get; set; }
        public HashSet<string> Paths { get; set; } = new HashSet<string>();
    }

    public class InvalidEndpointTrackerMiddleware
    {
        private const int MaxAttempts = 10;

        //Something needs to be done about clearing these out
        private static readonly Dictionary<string, AttemptInfo> _failedAttempts = new Dictionary<string, AttemptInfo>();

        private static readonly System.TimeSpan cleanupInterval = System.TimeSpan.FromHours(24);

        // Set your threshold here
        private static System.DateTime _lastCleanup = System.DateTime.UtcNow;

        private readonly RequestDelegate _next;

        public InvalidEndpointTrackerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            CleanupFailedAttempts(); // Check and perform cleanup if needed

            string clientIP = APIUtility.GetClientPublicIPAddress(context);
            string requestedPath = context.Request.Path;

            if (IPBlacklist.IsIPBlocked(clientIP))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            // In ASP.NET Core 2.x, we need to track the original response body
            // to detect if the next middleware wrote anything
            Stream originalBodyStream = context.Response.Body;
            long originalResponseLength = 0;
            
            try
            {
                // Create a temporary stream to capture response
                using (MemoryStream responseBody = new System.IO.MemoryStream())
                {
                    context.Response.Body = responseBody;
                    
                    await _next(context);
                    
                    // Check if any content was written
                    originalResponseLength = responseBody.Length;
                    
                    // Copy the response back to the original stream
                    responseBody.Seek(0, System.IO.SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }

            // In ASP.NET Core 2.x, a truly non-existent endpoint typically results in:
            // - 404 status code
            // - No response body (or default 404 body)
            // - No controller/action was executed
            // - Exclude root path "/" as it's valid to hit even without an explicit endpoint
            bool isNonExistentEndpoint = context.Response.StatusCode == StatusCodes.Status404NotFound &&
                                         originalResponseLength == 0 &&
                                         requestedPath != "/";

            // Only track if we believe this is a non-existent endpoint
            if (isNonExistentEndpoint)
            {
                RecordFailedAttempt(clientIP, requestedPath);
            }
            else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                IPBlacklistMiddleware.AddLog(
                    LogMessage.Informational(
                        $"{clientIP} attempted to access {requestedPath} and was not authorized."
                    )
                );
                RecordFailedAttempt(clientIP, requestedPath);
            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                IPBlacklistMiddleware.AddLog(
                    LogMessage.Informational(
                        $"{clientIP} attempted to access {requestedPath} and was forbidden."
                    )
                );
                RecordFailedAttempt(clientIP, requestedPath);
            }
        }

        private static void CleanupFailedAttempts()
        {
            if ((System.DateTime.UtcNow - _lastCleanup) > cleanupInterval)
            {
                _failedAttempts.Clear(); // Clears the dictionary of failed attempts
                _lastCleanup = System.DateTime.UtcNow; // Reset the last cleanup time
                IPBlacklistMiddleware.ClearLogs();
                IPBlacklistMiddleware.AddLog(LogMessage.Informational($"_failedAttempts and logs was cleared out!"));
            }
        }

        private static void RecordFailedAttempt(string ipAddress, string requestedPath)
        {
            if (requestedPath.EndsWith(".env"))
            {
                IPBlacklist.AddBannedIP(ipAddress, $"Blocked for unauthorized access attempt onto secure path: {requestedPath}.");
                IPBlacklistMiddleware.AddLog(LogMessage.Informational($"{ipAddress} tried to access {requestedPath} which ended in '.env'. IP has been banned."));
                return;
            }

            if (!_failedAttempts.ContainsKey(ipAddress))
            {
                _failedAttempts[ipAddress] = new AttemptInfo();
            }

            // Increment the count and add the path to the HashSet
            _failedAttempts[ipAddress].Count++;
            _failedAttempts[ipAddress].Paths.Add(requestedPath);

            // Log the attempt with count and unique paths
            IPBlacklistMiddleware.AddLog(
                LogMessage.Informational(
                    $"{ipAddress} attempted to access {requestedPath}. Total attempts: {_failedAttempts[ipAddress].Count}. Total distinct paths: {_failedAttempts[ipAddress].Paths.Count}."
                )
            );

            // Check if the total number of attempts reaches the threshold
            if (_failedAttempts[ipAddress].Count >= MaxAttempts)
            {
                // Concatenate all paths for logging or blocking reason
                string allPaths = string.Join(", ", _failedAttempts[ipAddress].Paths);
                IPBlacklist.AddBannedIP(
                    ipAddress,
                    $"Blocked after repeated unauthorized attempts on paths: {allPaths}. Total failed attempts: {_failedAttempts[ipAddress].Count}."
                );
            }
        }

        // Method to clear failed attempts (for testing purposes)
        public static void ClearFailedAttempts()
        {
            _failedAttempts.Clear();
        }
    }
}