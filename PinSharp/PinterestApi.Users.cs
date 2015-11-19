using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IUsersApi
    {
        public async Task<dynamic> GetUserAsync(string userName, IEnumerable<string> fields)
        {
            return await GetUserAsync<dynamic>(userName, fields);
        }

        public async Task<UserDetails> GetUserAsync(string userName)
        {
            return await GetUserAsync<UserDetails>(userName, UserFields);
        }

        private async Task<T> GetUserAsync<T>(string userName, IEnumerable<string> fields)
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/users/{userName}/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<T>();
            }
        }

    }
}
