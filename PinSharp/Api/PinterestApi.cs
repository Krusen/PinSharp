using System.Collections.Generic;
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

        private async Task<T> GetAsync<T>(string path, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.GetAsync(path).Configured())
            {
                var content = await response.Content.ReadAsAsync<dynamic>().Configured();
                return JsonConvert.DeserializeObject<T>(content.data.ToString());
            }
        }

        private async Task<PagedApiResponse<IEnumerable<T>>> GetPagedAsync<T>(string path, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.GetAsync(path).Configured())
            {
                var content = await response.Content.ReadAsStringAsync().Configured();
                return JsonConvert.DeserializeObject<PagedApiResponse<IEnumerable<T>>>(content);
            }
        }

        private async Task PostAsync(string path, object value)
        {
            await PostAsyncInternal(path, value).Configured();
        }

        private async Task<T> PostAsync<T>(string path, object value, RequestOptions options = null)
        {
            var content = await PostAsyncInternal(path, value, options).Configured();
            return JsonConvert.DeserializeObject<T>(content.data.ToString());
        }

        private async Task<dynamic> PostAsyncInternal(string path, object value, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.PostAsync(path, value).Configured())
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<dynamic>().Configured();
                    if (error.type == "api")
                        throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                    response.EnsureSuccessStatusCode();
                }
                return await response.Content.ReadAsAsync<dynamic>().Configured();
            }
        }

        private async Task<T> PatchAsync<T>(string path, object value, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.PatchAsync(path, value).Configured())
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<dynamic>().Configured();
                    if (error.type == "api")
                        throw new PinterestApiException(error.message.ToString()) { Type = error.type, Param = error.param };
                    response.EnsureSuccessStatusCode();
                }
                var content = await response.Content.ReadAsAsync<dynamic>().Configured();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        private async Task DeleteAsync(string path)
        {
            using (var response = await Client.DeleteAsync($"{path}/").Configured())
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
