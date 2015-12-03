using System.Linq;

namespace PinSharp.Api
{
    internal class PathBuilder
    {
        public static string BuildPath(string basePath, RequestOptions options)
        {
            var path = basePath;

            if (!path.EndsWith("/"))
                path += "/";

            if (options?.SearchQuery != null)
                path = path.AddQueryParam("query", options.SearchQuery);

            if (options?.Fields?.Any() == true)
            {
                var fields = string.Join(",", options.Fields);
                path = path.AddQueryParam("fields", fields);
            }

            if (options?.Cursor != null)
                path = path.AddQueryParam("cursor", options.Cursor);

            if (options?.Limit > 0)
                path = path.AddQueryParam("limit", options.Limit);

            return path;
        }
    }

    internal static class QueryStringExtensions
    {
        public static string AddQueryParam(this string original, string name, object value)
        {
            original += original.Contains("?") ? "&" : "?";
            original += $"{name}={value}";
            return original;
        }
    }
}
