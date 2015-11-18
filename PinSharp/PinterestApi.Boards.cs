using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IBoardsApi
    {
        public async Task<BoardDetails> GetBoardAsync(string userName, string boardName)
        {
            return await GetBoardAsync($"{userName}/{boardName}");
        }

        public async Task<IEnumerable<Pin>> GetPinsAsync(string userName, string boardName, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync($"{userName}/{boardName}", imageSizes, limit, cursor);
        }

        public async Task<BoardDetails> GetBoardAsync(string boardIdOrPath)
        {
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/boards/{boardIdOrPath}/", BoardFields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<BoardDetails>();
            }
        }

        // TODO: Implement limit and paging
        public async Task<IEnumerable<Pin>> GetPinsAsync(string boardIdOrPath, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null)
        {
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/boards/{boardIdOrPath}/pins/", PinFields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<IEnumerable<Pin>>();
            }
        }
    }
}
