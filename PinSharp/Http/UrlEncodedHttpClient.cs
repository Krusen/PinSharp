using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PinSharp.Extensions;

namespace PinSharp.Http
{
    public class UrlEncodedHttpClient : IHttpClient
    {
        private HttpClient Client { get; }

        public UrlEncodedHttpClient(string baseAddress, string accessToken)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            Client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
                }
            };
        }

        public virtual Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return Client.GetAsync(requestUri);
        }

        public virtual Task<HttpResponseMessage> PostAsync<T>(string requestUri, T value)
        {
            var content = GetFormUrlEncodedContent(value);
            return Client.PostAsync(requestUri, content);
        }

        public virtual Task<HttpResponseMessage> PatchAsync<T>(string requestUri, T value)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri);
            request.Headers.ExpectContinue = false;
            request.Content = GetFormUrlEncodedContent(value);
            return Client.SendAsync(request);
        }

        public virtual Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return Client.DeleteAsync(requestUri);
        }

        protected static FormUrlEncodedContent GetFormUrlEncodedContent(object obj)
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
