using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PinSharp.Extensions;
using System.Net;

namespace PinSharp.Http
{
    public class WebProxy : IWebProxy
    {
        private readonly Uri _uri;

        public WebProxy(string ip, int port)
        {
            _uri = new Uri("http://" + ip + ":" + port);
        }
        public Uri GetProxy(Uri destination) => _uri;

        public bool IsBypassed(Uri host) => false;

        public ICredentials Credentials { get; set; }
    }

    internal class UrlEncodedHttpClient : IHttpClient
    {
        private HttpClient Client { get; }

     public UrlEncodedHttpClient(string baseAddress, string accessToken, string proxyIp, string port, string userName, string password)
        {
            if (proxyIp != "")
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    Proxy = new WebProxy(proxyIp, Convert.ToInt32(port)),
                    Credentials = new NetworkCredential(userName, password),
                    UseProxy = true,
                };

                Client = new HttpClient(handler);
            }
            else
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
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri);
            request.Headers.ExpectContinue = false;
            request.Content = GetFormUrlEncodedContent(value);
            return Client.SendAsync(request);
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
