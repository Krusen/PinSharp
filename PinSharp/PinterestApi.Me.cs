using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IMeApi
    {
        public async Task<UserDetails> GetUserAsync()
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/me/", UserFields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<UserDetails>();
            }
        }

        public async Task<IEnumerable<UserBoard>> GetBoardsAsync()
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/me/boards/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<IEnumerable<UserBoard>>();
            }
        }

        // TODO: Implement limit and paging
        public async Task<IEnumerable<UserPin>> GetPinsAsync(int limit = 0, string cursor = null)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/me/pins/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<IEnumerable<UserPin>>();
            }
        }

        // TODO: Implement limit and paging
        public async Task<IEnumerable<UserPin>> GetLikedPinsAsync(int limit = 0, string cursor = null)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/me/pins/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<IEnumerable<UserPin>>();
            }
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Board>> GetSuggestedBoardsAsync(string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Board>> GetFollowingBoardsAsync(string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Interest>> GetFollowingInterestsAsync(string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetFollowingUsersAsync(string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserBoard>> SearchBoards(string query, string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserPin>> SearchPins(string query, string cursor = null)
        {
            throw new NotImplementedException();
        }
    }
}
