using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Extensions;
using PinSharp.Models.Exceptions;
using PinSharp.Models.Responses;

namespace PinSharp
{
    public partial class PinterestApi : IBoardsApi, IMeApi, IPinsApi, IUsersApi
    {
        protected MediaTypeFormatter JsonFormatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }
        };

        protected HttpClient Client { get; }

        private const string BaseUrl = "https://api.pinterest.com";

        internal string AccessToken { get; set; }
        internal string ApiVersion { get; set; }

        internal PinterestApi(string accessToken, string apiVersion)
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;

            Client = new HttpClient {BaseAddress = new Uri($"{BaseUrl}/{ApiVersion}/"),};
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        }

        protected static string GetPathWithFieldsLimitAndCursor(string path, IEnumerable<string> fields, string cursor = null, int limit = 0)
        {
            if (!path.Contains("?"))
                path = path.EnsurePostfix("/");

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

        protected static string AddQueryParam(string original, string name, object value)
        {
            original += original.Contains("?") ? "&" : "?";
            original += $"{name}={value}";
            return original;
        }

        protected async Task<T> Get<T>(string path)
        {
            return await Get<T>(path, Enumerable.Empty<string>());
        }

        protected async Task<T> Get<T>(string path, IEnumerable<string> fields)
        {
            path = GetPathWithFieldsLimitAndCursor(path, fields);

            using (var response = await Client.GetAsync(path))
            {
                var json = await response.Content.ReadAsStringAsync();
                var jtoken = JsonConvert.DeserializeObject<JToken>(json);
                return jtoken.SelectToken("data").ToObject<T>();
            }
        }

        protected async Task<PagedApiResponse<IEnumerable<T>>> GetPaged<T>(string path, string cursor, int limit)
        {
            return await GetPaged<T>(path, null, cursor, limit);
        }

        protected async Task<PagedApiResponse<IEnumerable<T>>> GetPaged<T>(string path, IEnumerable<string> fields, string cursor, int limit)
        {
            path = GetPathWithFieldsLimitAndCursor(path, fields, cursor, limit);

            using (var response = await Client.GetAsync(path))
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PagedApiResponse<IEnumerable<T>>>(json);
            }
        }

        protected async Task Post(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFieldsLimitAndCursor(path, fields);
            var content = new ObjectContent<object>(value, JsonFormatter);

            using (var response = await Client.PostAsync(path, content))
            {

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<dynamic>();
                    if (error.type == "api")
                        throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        protected async Task<T> Post<T>(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFieldsLimitAndCursor(path, fields);
            var content = new ObjectContent<object>(value, JsonFormatter);

            using (var response = await Client.PostAsync(path, content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<dynamic>();
                    if (error.type == "api")
                        throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                    response.EnsureSuccessStatusCode();
                }
                var json = await response.Content.ReadAsStringAsync();
                var jtoken = JsonConvert.DeserializeObject<JToken>(json);
                return jtoken.SelectToken("data").ToObject<T>();
            }
        }

        protected async Task Patch(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFieldsLimitAndCursor(path, fields);

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), path))
            {
                request.Content = new ObjectContent<object>(value, JsonFormatter);

                using (var response = await Client.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsAsync<dynamic>();
                        if (error.type == "api")
                            throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
        }

        protected async Task<T> Patch<T>(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFieldsLimitAndCursor(path, fields);

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), path))
            {
                request.Content = new ObjectContent<object>(value, JsonFormatter);

                using (var response = await Client.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsAsync<dynamic>();
                        if (error.type == "api")
                            throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                        response.EnsureSuccessStatusCode();
                    }
                    var json = await response.Content.ReadAsStringAsync();
                    var jtoken = JsonConvert.DeserializeObject<JToken>(json);
                    return jtoken.SelectToken("data").ToObject<T>();
                }
            }
        }

        protected async Task Delete(string path)
        {
            using (var response = await Client.DeleteAsync($"{path}/"))
            {
                response.EnsureSuccessStatusCode();
            }
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
