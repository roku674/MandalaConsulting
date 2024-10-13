//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using System;
using System.Collections.Generic;
using MandalaConsulting.Optimization.Logging;

namespace MandalaConsulting.APIMiddleware.Objects
{
    public static class IPBlacklist
    {
        private static readonly Dictionary<string, string> blacklistedIPs = new Dictionary<string, string>();

        // Event declaration for when a new IP is added
        public static event EventHandler<BannedIP> IPBanned;

        // Method to add a banned IP with a reason
        public static void AddBannedIP(string ip, string reason)
        {
            if (!blacklistedIPs.ContainsKey(ip))
            {
                blacklistedIPs[ip] = reason;
                IPBanned?.Invoke(null, new BannedIP(null, ip, null, reason));
            }
        }

        // Check if an IP is blocked and log the reason
        public static bool IsIPBlocked(string ipAddress)
        {
            if (blacklistedIPs.ContainsKey(ipAddress))
            {
                string reason = blacklistedIPs[ipAddress];
                IPBlacklistMiddleware.AddLog(LogMessage.Warning($"{ipAddress} was already blocked! Reason: {reason}"));
                return true;
            }
            return false;
        }

        // Optionally, provide a method to get the block reason for a specific IP
        public static string GetBlockReason(string ipAddress)
        {
            if (blacklistedIPs.TryGetValue(ipAddress, out string reason))
            {
                return reason;
            }
            return null; // Or "Not blocked" or any other indicator that the IP is not in the blacklist
        }
    }
}
