using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PinSharp.Api.Exceptions;
using PinSharp.Api.Responses;
using PinSharp.Http;

namespace PinSharp.Api
{
    internal partial class PinterestApi
    {
        private const string RateLimitHeader = "X-Ratelimit-Limit";
        private const string RateLimitRemainingHeader = "X-Ratelimit-Remaining";
        private static readonly object RateLimitsLock = new object();

        private const string BaseUrl = "https://api.pinterest.com";

        private IHttpClient Client { get; }

        public IRateLimits RateLimits { get; private set; }

        internal PinterestApi(string accessToken, string apiVersion)
        {
            Client = new UrlEncodedHttpClient($"{BaseUrl}/{apiVersion}/", accessToken);
        }

        internal PinterestApi(IHttpClient httpClient)
        {
            Client = httpClient;
        }

        // TODO: NOTE: Returns null if not found (404)
        private async Task<T> GetAsync<T>(string path, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.GetAsync(path).ConfigureAwait(false))
            {
                UpdateRateLimits(response.Headers);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return default(T);

                if (!response.IsSuccessStatusCode)
                    throw await CreateException(response).ConfigureAwait(false);

                var content = await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(content.data.ToString());
            }
        }

        // TODO: NOTE: Returns null if not found (404)
        private async Task<PagedApiResponse<IEnumerable<T>>> GetPagedAsync<T>(string path, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.GetAsync(path).ConfigureAwait(false))
            {
                UpdateRateLimits(response.Headers);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                if (!response.IsSuccessStatusCode)
                    throw await CreateException(response).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<PagedApiResponse<IEnumerable<T>>>(content);
            }
        }

        private async Task PostAsync(string path, object value)
        {
            await PostAsyncInternal(path, value).ConfigureAwait(false);
        }

        private async Task<T> PostAsync<T>(string path, object value, RequestOptions options = null)
        {
            var content = await PostAsyncInternal(path, value, options).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content.data.ToString());
        }

        private async Task<dynamic> PostAsyncInternal(string path, object value, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.PostAsync(path, value).ConfigureAwait(false))
            {
                UpdateRateLimits(response.Headers);

                if (!response.IsSuccessStatusCode)
                    throw await CreateException(response).ConfigureAwait(false);

                return await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
            }
        }

        private async Task<T> PatchAsync<T>(string path, object value, RequestOptions options = null)
        {
            path = PathBuilder.BuildPath(path, options);

            using (var response = await Client.PatchAsync(path, value).ConfigureAwait(false))
            {
                UpdateRateLimits(response.Headers);

                if (!response.IsSuccessStatusCode)
                    throw await CreateException(response).ConfigureAwait(false);

                var content = await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(content.data.ToString());
            }
        }

        private async Task DeleteAsync(string path)
        {
            using (var response = await Client.DeleteAsync($"{path}/").ConfigureAwait(false))
            {
                UpdateRateLimits(response.Headers);

                if (!response.IsSuccessStatusCode)
                    throw await CreateException(response).ConfigureAwait(false);
            }
        }

        private void UpdateRateLimits(HttpHeaders headers)
        {
            if (!headers.Contains(RateLimitHeader)) return;
            if (!headers.Contains(RateLimitRemainingHeader)) return;

            var limit = headers.GetValues(RateLimitHeader).First();
            var remaining = headers.GetValues(RateLimitRemainingHeader).First();

            lock (RateLimitsLock)
            {
                var now = DateTimeOffset.Now;

                if (RateLimits?.LastUpdated >= now)
                    return;

                RateLimits = new RateLimits(int.Parse(limit), int.Parse(remaining), now);
            }
        }

        private static async Task<Exception> CreateException(HttpResponseMessage response)
        {
            var url = response.RequestMessage.RequestUri.ToString();
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var error = await response.Content.ReadAsAsync<ErrorResponse>().ConfigureAwait(false);
            var message = error.Message;
            var status = (int)response.StatusCode;
            switch (status)
            {
                case 400:
                    return PinSharpException.Create<PinSharpBadRequestException>(message, url, content);
                case 401:
                    return PinSharpException.Create<PinSharpAuthorizationException>(message, url, content);
                case 403:
                    return PinSharpException.Create<PinSharpForbiddenException>(message, url, content);
                case 404:
                    return PinSharpException.Create<PinSharpNotFoundException>(message, url, content);
                case 408:
                    return PinSharpException.Create<PinSharpTimeoutException>(message, url, content);
                case 429:
                    return PinSharpException.Create<PinSharpRateLimitExceededException>(message, url, content, 429);
                case 500:
                case 502:
                case 599:
                    return PinSharpException.Create<PinSharpServerErrorException>(message, url, content, status);
                default:
                    return new PinSharpException(message)
                    {
                        RequestUrl = url,
                        ResponseContent = content,
                        HttpStatusCode = status
                    };
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
