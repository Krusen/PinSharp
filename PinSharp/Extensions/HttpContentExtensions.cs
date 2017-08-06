using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PinSharp.Extensions
{
    internal static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            if (content.Headers.ContentLength == 0)
                return default(T);

            var data = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
