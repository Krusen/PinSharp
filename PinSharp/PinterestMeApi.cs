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
    public class PinterestMeApi : PinterestApiBase
    {
        private static string[] PinFields => new[]
        {
            "id",
            "link",
            "note",
            "url",
            "attribution",
            "original_link",
            "color",
            "board",
            "counts",
            "created_at",
            "image",
            "media",
            "metadata"
        };

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

        private static string[] BoardFields => new[]
        {
            "id",
            "url",
            "name",
            "created_at",
            "counts",
            "description",
            "reason",
            "privacy",
            "image"
        };

        public PinterestMeApi(string accessToken, string apiVersion)
            : base(accessToken, apiVersion)
        {
        }

        public async Task<UserDetails> GetUserAsync()
        {
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/", UserFields);

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
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/boards/", BoardFields);

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
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/pins/", PinFields);

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
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/pins/", PinFields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<IEnumerable<UserPin>>();
            }
        }

        public async Task<IEnumerable<User>>  GetFollowersAsync(string cursor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Board>>  GetSuggestedBoardsAsync(string cursor = null)
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
