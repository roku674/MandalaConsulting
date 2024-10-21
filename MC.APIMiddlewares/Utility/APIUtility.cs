//Copyright © 2024 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using MandalaConsulting.Optimization.Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Linq;

namespace MandalaConsulting.APIMiddleware.Utility
{
    public class APIUtility
    {
        public static string GetClientPublicIPAddress(HttpContext context)
        {
            try
            {
                string ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = context.Connection.RemoteIpAddress.ToString();
                }

                if (ipAddress == "::1")
                {
                    IPBlacklistMiddleware.AddLog(
                        LogMessage.Critical(
                            $" There was a problem getting the IP Address from {context.ToString()}. It was assigned it an arbitrary 198.51.100.255"
                        )
                    );
                    return "198.51.100.255";
                }

                return ipAddress;
            }
            catch (System.Exception ex)
            {
                IPBlacklistMiddleware.AddLog(
                    LogMessage.Critical(
                        $" There was a problem getting the IP Address from {context.ToString()}. It was assigned it an arbitrary 198.51.100.255 {ex.ToString()}"
                    )
                );
                return "198.51.100.255";
            }
        }

        public static string GetClientPublicIPAddress(ActionExecutingContext context)
        {
            string ipAddress = string.Empty;

            // Check if the X-Forwarded-For header has any value
            if (context.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = context.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            }

            // If X-Forwarded-For is empty, use the remote IP address
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            if (ipAddress == "::1")
            {
                IPBlacklistMiddleware.AddLog(
                    LogMessage.Critical(
                        $" There was a problem getting the IP Address from {context.ToString()}. It was assigned it an arbitrary 198.51.100.255"
                    )
                );
                return "198.51.100.255";
            }

            return ipAddress;
        }
    }
}