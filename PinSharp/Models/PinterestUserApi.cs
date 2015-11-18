using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PinSharp.Models
{
    public class PinterestUserApi : PinterestApiBase
    {
        public PinterestUserApi(string accessToken, string apiVersion)
            : base(accessToken, apiVersion)
        {
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

        public async Task<UserDetails> GetUserAsync(string userName)
        {
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/users/{userName}/", UserFields);

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
