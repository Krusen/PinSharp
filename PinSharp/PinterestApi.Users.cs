using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IUsersApi
    {
        public async Task<UserDetails> GetUserAsync(string userName)
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/users/{userName}/", UserFields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<UserDetails>();
            }
        }

    }
}
