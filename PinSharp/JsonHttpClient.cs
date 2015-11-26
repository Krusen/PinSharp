using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PinSharp
{
    public class JsonHttpClient : IHttpClient
    {
        private HttpClient Client { get; }

        private MediaTypeFormatter MediaTypeFormatter { get; }

        public JsonHttpClient(string baseAddress, string accessToken)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            MediaTypeFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}
            };
        }

        public JsonHttpClient(string baseAddress, string accessToken, MediaTypeFormatter mediaTypeFormatter)
            : this(baseAddress, accessToken)
        {
            MediaTypeFormatter = mediaTypeFormatter;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await Client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T value)
        {
            var content = new ObjectContent<object>(value, MediaTypeFormatter);
            return await Client.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PatchAsync<T>(string requestUri, T value)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri))
            {
                request.Content = new ObjectContent<object>(value, MediaTypeFormatter);
                return await Client.SendAsync(request);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await Client.DeleteAsync(requestUri);
        }
    }
}