using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Extensions;
using PinSharp.Models.Exceptions;

namespace PinSharp
{
    public partial class PinterestApi : IBoardsApi, IMeApi, IPinsApi, IUsersApi
    {
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

        protected static string GetPathWithFields(string path, IEnumerable<string> fields)
        {
            var hasQuery = path.Contains("?");
            var paramSeparator = hasQuery ? "&" : "?";

            if (!hasQuery)
                path = path.EnsurePostfix("/");

            if (fields?.Any() == true)
            {
                var fieldsString = string.Join(",", fields);
                path += $"{paramSeparator}fields={fieldsString}";
            }

            return path;
        }

        protected async Task<T> Get<T>(string path)
        {
            return await Get<T>(path, Enumerable.Empty<string>());
        }

        protected async Task<T> Get<T>(string path, IEnumerable<string> fields)
        {
            path = GetPathWithFields(path, fields);

            using (var response = await Client.GetAsync($"{path}"))
            {
                var json = await response.Content.ReadAsStringAsync();
                var jtoken = JsonConvert.DeserializeObject<JToken>(json);
                return jtoken.SelectToken("data").ToObject<T>();
            }
        }

        protected async Task Post(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFields(path, fields);

            var response = await Client.PostAsJsonAsync($"{path}", value);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsAsync<dynamic>();
                if (error.type == "api")
                    throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                response.EnsureSuccessStatusCode();
            }
        }

        protected async Task<TResponse> Post<TResponse>(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFields(path, fields);

            var response = await Client.PostAsJsonAsync($"{path}/", value);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsAsync<dynamic>();
                if (error.type == "api")
                    throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                response.EnsureSuccessStatusCode();
            }
            var json = await response.Content.ReadAsStringAsync();
            var jtoken = JsonConvert.DeserializeObject<JToken>(json);
            return jtoken.SelectToken("data").ToObject<TResponse>();
        }

        protected async Task Patch(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFields(path, fields);

            throw new NotImplementedException();
        }

        protected async Task<TResponse> Patch<TResponse>(string path, object value, IEnumerable<string> fields = null)
        {
            path = GetPathWithFields(path, fields);

            throw new NotImplementedException();
        }

        protected async Task Delete(string path)
        {
            var response = await Client.DeleteAsync($"{path}/");
            response.EnsureSuccessStatusCode();
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
