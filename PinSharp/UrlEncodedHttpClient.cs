using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PinSharp.Extensions;

namespace PinSharp
{
    internal class UrlEncodedHttpClient : IHttpClient
    {
        private HttpClient Client { get; }

        public UrlEncodedHttpClient(string baseAddress, string accessToken)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return Client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string requestUri, T value)
        {
            var content = GetFormUrlEncodedContent(value);
            return Client.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PatchAsync<T>(string requestUri, T value)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri))
            {
                request.Headers.ExpectContinue = false;
                request.Content = GetFormUrlEncodedContent(value);
                return Client.SendAsync(request);
            }
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return Client.DeleteAsync(requestUri);
        }

        private static FormUrlEncodedContent GetFormUrlEncodedContent(object obj)
        {
            // TODO: Add attribute to ignore property?
            var data =
                obj.GetType()
                    .GetProperties()
                    .Select(prop => new KeyValuePair<string, string>(prop.Name, prop.GetValue(obj, null)?.ToString()))
                    .Where(x => x.Value != null);

            return new FormUrlEncodedContent(data);
        }
    }
}
