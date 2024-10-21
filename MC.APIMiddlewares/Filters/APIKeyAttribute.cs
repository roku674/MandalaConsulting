//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using MandalaConsulting.APIMiddleware.Utility;
using MandalaConsulting.Optimization.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace MandalaConsulting.APIMiddleware.Filters
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
    public class APIKeyAttribute : System.Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string apiKeyHeaderName = GetKeyName();

            Microsoft.Extensions.Primitives.StringValues potentialKey = "";
            string clientIP = APIUtility.GetClientPublicIPAddress(context);

            // Try to retrieve the API key from the request headers
            if (!context.HttpContext.Request.Headers.TryGetValue(apiKeyHeaderName, out potentialKey))
            {
                IPBlacklistMiddleware.AddLog(LogMessage.Warning($"API Key not found in header '{apiKeyHeaderName}' from IP {clientIP}!")); // Log warning with IP and the attempted header name if key is missing
                context.Result = new UnauthorizedResult();
                return;
            }

            string correctApiKey = GetKey();

            // Check if the provided key matches the correct key
            if (!correctApiKey.Equals(potentialKey))
            {
                IPBlacklistMiddleware.AddLog(LogMessage.Warning($"IP {clientIP} provided incorrect key '{potentialKey}' in header '{apiKeyHeaderName}'!")); // Log warning with IP, the attempted key, and the header name if key is incorrect
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }

        internal string GetKey()
        {
            string apikey = System.Environment.GetEnvironmentVariable("API_KEY");
            return apikey;
        }

        internal string GetKeyName()
        {
            string keyName = System.Environment.GetEnvironmentVariable("API_KEY_NAME");
            return keyName;
        }
    }
}