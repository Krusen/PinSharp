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
        public async Task<dynamic> GetBoardAsync(string board, IEnumerable<string> fields)
        {
            return await GetBoardAsync<dynamic>(board, fields);
        }

        public async Task<dynamic> GetBoardAsync(string userName, string boardName, IEnumerable<string> fields)
        {
            return await GetBoardAsync($"{userName}/{boardName}", fields);
        }

        public async Task<BoardDetails> GetBoardAsync(string board)
        {
            return await GetBoardAsync<BoardDetails>(board, BoardFields);
        }

        public async Task<BoardDetails> GetBoardAsync(string userName, string boardName)
        {
            return await GetBoardAsync($"{userName}/{boardName}");
        }

        private async Task<T> GetBoardAsync<T>(string board, IEnumerable<string> fields)
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/boards/{board}/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<T>();
            }
        }

        public async Task<IEnumerable<dynamic>> GetPinsAsync(string board, IEnumerable<string> fields, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync<dynamic>(board, fields, limit, cursor);
        }

        public async Task<IEnumerable<dynamic>> GetPinsAsync(string userName, string boardName, IEnumerable<string> fields, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync($"{userName}/{boardName}", fields, limit, cursor);
        }

        public async Task<IEnumerable<Pin>> GetPinsAsync(string userName, string boardName, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync($"{userName}/{boardName}", imageSizes, limit, cursor);
        }

        // TODO: Do something about imageSizes
        public async Task<IEnumerable<Pin>> GetPinsAsync(string board, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync<Pin>(board, PinFields, limit, cursor);
        }

        // TODO: Implement limit and paging
        private async Task<IEnumerable<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit = 0, string cursor = null)
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/boards/{board}/pins/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<IEnumerable<T>>();
            }
        }
    }
}
