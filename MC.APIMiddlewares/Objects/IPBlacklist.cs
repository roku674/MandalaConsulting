// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-13 01:57:25
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields
using MandalaConsulting.Optimization.Logging;
using System;
using System.Collections.Generic;

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

        // Optionally, provide a method to get the block reason for a specific IP
        public static string GetBlockReason(string ipAddress)
        {
            if (blacklistedIPs.TryGetValue(ipAddress, out string reason))
            {
                return reason;
            }
            return null; // Or "Not blocked" or any other indicator that the IP is not in the blacklist
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

        // Method to clear all blacklisted IPs (for testing purposes)
        public static void ClearBlacklist()
        {
            blacklistedIPs.Clear();
            // Clear all event handlers
            IPBanned = null;
        }
    }
}
