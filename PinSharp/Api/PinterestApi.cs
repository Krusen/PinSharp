using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PinSharp.Api.Exceptions;
using PinSharp.Api.Responses;

namespace PinSharp.Api
{
    public partial class PinterestApi : IBoardsApi, IMeApi, IPinsApi, IUsersApi
    {
        private IHttpClient Client { get; }

        private const string BaseUrl = "https://api.pinterest.com";

        internal PinterestApi(string accessToken, string apiVersion)
        {
            Client = new JsonHttpClient($"{BaseUrl}/{apiVersion}/", accessToken);
        }

        internal PinterestApi(IHttpClient httpClient)
        {
            Client = httpClient;
        }

        private async Task<T> Get<T>(string path, IEnumerable<string> fields = null)
        {
            path = BuildPath(path, fields);

            using (var response = await Client.GetAsync(path))
            {
                var content = await response.Content.ReadAsAsync<dynamic>();
                return JsonConvert.DeserializeObject<T>(content.data.ToString());
            }
        }

        private async Task<PagedApiResponse<IEnumerable<T>>> GetPaged<T>(string path, string cursor, int limit)
        {
            return await GetPaged<T>(path, null, cursor, limit);
        }

        private async Task<PagedApiResponse<IEnumerable<T>>> GetPaged<T>(string path, IEnumerable<string> fields, string cursor, int limit)
        {
            path = BuildPath(path, fields, cursor, limit);

            using (var response = await Client.GetAsync(path))
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PagedApiResponse<IEnumerable<T>>>(content);
            }
        }

        private async Task Post(string path, object value, IEnumerable<string> fields = null)
        {
            await PostInternal(path, value, fields);
        }

        private async Task<T> Post<T>(string path, object value, IEnumerable<string> fields = null)
        {
            var content = await PostInternal(path, value, fields);
            return JsonConvert.DeserializeObject<T>(content.data.ToString());
        }

        private async Task<dynamic> PostInternal(string path, object value, IEnumerable<string> fields = null)
        {
            path = BuildPath(path, fields);

            using (var response = await Client.PostAsync(path, value))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<dynamic>();
                    if (error.type == "api")
                        throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                    response.EnsureSuccessStatusCode();
                }
                return response.Content.ReadAsAsync<dynamic>();
            }
        }

        private async Task<T> Patch<T>(string path, object value, IEnumerable<string> fields = null)
        {
            path = BuildPath(path, fields);

            using (var response = await Client.PatchAsync(path, value))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<dynamic>();
                    if (error.type == "api")
                        throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                    response.EnsureSuccessStatusCode();
                }
                var content = await response.Content.ReadAsAsync<dynamic>();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        private async Task DeleteAsync(string path)
        {
            using (var response = await Client.DeleteAsync($"{path}/"))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        private static string BuildPath(string path, IEnumerable<string> fields, string cursor = null, int limit = 0)
        {
            if (!path.Contains("?") && !path.EndsWith("/"))
                path += "/";

            if (fields?.Any() == true)
            {
                var fieldsString = string.Join(",", fields);
                path = AddQueryParam(path, "fields", fieldsString);
            }

            if (limit > 0)
                path = AddQueryParam(path, "limit", limit);

            if (cursor != null)
                path = AddQueryParam(path, "cursor", cursor);

            return path;
        }

        private static string AddQueryParam(string original, string name, object value)
        {
            original += original.Contains("?") ? "&" : "?";
            original += $"{name}={value}";
            return original;
        }

        private static string[] UserFields => new[]
        {
            "id",
            "username",
            "first_name",
            "last_name",
            "url",
            "created_at",
            "counts",
            "account_type",
            "bio",
            "image"
        };

        private static string[] PinFields => new[]
        {
            "id",
            "url",
            "link",
            "note",
            "attribution",
            "original_link",
            "color",
            "board",
            "counts",
            "created_at",
            "creator(id,url,first_name,last_name,username,image)",
            "image",
            "media",
            "metadata"
        };

        private static string[] BoardFields => new[]
        {
            "id",
            "url",
            "name",
            "created_at",
            "creator(id,url,first_name,last_name,username,image)",
            "counts",
            "description",
            "reason",
            "privacy",
            "image"
        };
    }
}
