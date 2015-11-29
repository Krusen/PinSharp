using System.Net.Http;
using System.Threading.Tasks;

namespace PinSharp
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync<T>(string requestUri, T value);
        Task<HttpResponseMessage> PatchAsync<T>(string requestUri, T value);
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}
