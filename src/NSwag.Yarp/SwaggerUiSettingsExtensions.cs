using System.Linq;
using NSwag.AspNetCore;

namespace Microsoft.AspNetCore.Builder;

public static class SwaggerUiSettingsExtensions
{
    /// <summary>
    /// Enables NSwag to rewrite all of its paths whether called from YARP or directly.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="prefix">The prefix to add to all paths if NSwag is being called through YARP.</param>
    public static void AddYarp(this SwaggerUiSettings settings, string prefix)
    {
        settings.TransformToExternalPath = (internalUiRoute, request) =>
        {
            var isForwarded = request.Headers["X-Forwarded-Host"].Count != 0;

            if (!isForwarded)
            {
                return internalUiRoute;
            }

            if (!prefix.StartsWith("/"))
            {
                prefix = '/' + prefix;
            }

            prefix = prefix.TrimEnd('/');

            return prefix + internalUiRoute;
        };
    }

    /// <summary>
    /// Enables NSwag to rewrite all of its paths whether called from YARP or directly.
    /// The prefix will be determined using the X-Forwarded-Prefix header.
    /// </summary>
    /// <param name="settings"></param>
    public static void AddYarpWithForwardedPrefix(this SwaggerUiSettings settings)
    {
        settings.TransformToExternalPath = (internalUiRoute, request) =>
        {
            if (!request.Headers.TryGetValue("X-Forwarded-Prefix", out var headerValue))
            {
                return internalUiRoute;
            }

            var prefix = headerValue.FirstOrDefault();

            if (prefix is null)
            {
                return internalUiRoute;
            }

            if (!prefix.StartsWith("/"))
            {
                prefix = '/' + prefix;
            }

            prefix = prefix.TrimEnd('/');

            return prefix + internalUiRoute;
        };
    }
}
