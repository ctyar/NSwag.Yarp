using System.Linq;
using NSwag.AspNetCore;

namespace Microsoft.AspNetCore.Builder;

public static class OpenApiDocumentMiddlewareSettingsExtensions
{
    /// <summary>
    /// Enables NSwag to rewrite all of its paths whether called from YARP or directly.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="prefix">The prefix to add to all paths if NSwag is being called through YARP.</param>
    public static void AddYarp(this OpenApiDocumentMiddlewareSettings settings, string prefix)
    {
        settings.PostProcess = (document, request) =>
        {
            var isForwarded = request.Headers["X-Forwarded-Host"].Count != 0;

            if (!isForwarded)
            {
                return;
            }

            if (!prefix.StartsWith("/"))
            {
                prefix = '/' + prefix;
            }

            prefix = prefix.TrimEnd('/');

            document.BasePath = prefix;
        };

        settings.CreateDocumentCacheKey = request => request.Headers["X-Forwarded-Proto"] + " " + request.Headers["X-Forwarded-Host"];
    }

    /// <summary>
    /// Enables NSwag to rewrite all of its paths whether called from YARP or directly.
    /// The prefix will be determined using the X-Forwarded-Prefix header.
    /// </summary>
    /// <param name="settings"></param>
    public static void AddYarpWithForwardedPrefix(this OpenApiDocumentMiddlewareSettings settings)
    {
        settings.PostProcess = (document, request) =>
        {
            if (!request.Headers.TryGetValue("X-Forwarded-Prefix", out var headerValue))
            {
                return;
            }

            var prefix = headerValue.FirstOrDefault();

            if (prefix is null)
            {
                return;
            }

            if (!prefix.StartsWith("/"))
            {
                prefix = '/' + prefix;
            }

            prefix = prefix.TrimEnd('/');

            document.BasePath = prefix;
        };

        settings.CreateDocumentCacheKey = request => request.Headers["X-Forwarded-Proto"] + " " + request.Headers["X-Forwarded-Host"];
    }
}
