using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Evolutional.Project.Admin.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpContextExtensions
    {
        public static string GetAuthorizationTokenFromRequest(this HttpContext context)
        {
            var token = context.GetHeaderValueFromRequest("Authorization");
            return string.IsNullOrEmpty(token) ? string.Empty : token.Replace("Bearer ", "");
        }

        public static string GetHeaderValueFromRequest(this HttpContext context, string headerKey)
        {
            IHeaderDictionary headerDictionary = null;
            if (context != null)
            {
                headerDictionary = context.Request?.Headers;
            }
            if (headerDictionary == null)
            {
                return string.Empty;
            }
            var stringValues = headerDictionary.FirstOrDefault(m => string.Equals(m.Key, headerKey, StringComparison.InvariantCultureIgnoreCase)).Value;
            if (string.IsNullOrEmpty(stringValues))
            {
                return string.Empty;
            }
            return stringValues;
        }
    }
}