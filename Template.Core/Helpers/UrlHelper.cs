using Microsoft.AspNetCore.Http;

namespace Template.Core.Helpers
{
    public static class UrlHelper
    {
        public static string GetFullUrl(HttpContext httpContext, int? id = null)
        {
            var schema = httpContext.Request.Scheme;
            var host = httpContext.Request.Host;
            var path = httpContext.Request.Path;

            return $"{schema}://{host}{path}" + (id != null ? $"/{id}" : "");
        }
    }
}
