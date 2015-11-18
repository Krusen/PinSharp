using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public class PinterestApi : IBoardsApi, IMeApi, IPinsApi, IUsersApi
    {
        internal string AccessToken { get; set; }
        internal string ApiVersion { get; set; }

        internal PinterestApi(string accessToken, string apiVersion)
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;
        }

        protected string GetUrlWithFields(string url, string[] fields)
        {
            var fieldsString = string.Join(",", fields);
            return $"{url}?access_token={AccessToken}&fields={fieldsString}";
        }

        #region IBoardsApi
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
        #endregion

        #region IMeApi
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
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/boards/", MeBoardFields);

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
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/pins/", MePinFields);

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
            var url = GetUrlWithFields($"https://api.pinterest.com/{ApiVersion}/me/pins/", MePinFields);

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
        #endregion

        #region IPinsApi
        public async Task<Pin> GetPinAsync(string id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUsersApi
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
        #endregion

        private static string[] MePinFields => new[]
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

        private static string[] MeBoardFields => new[]
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

        private static string[] PinFields => new[]
{
            "id",
            "url",
            "link",
            "note",
            "attribution",
            "original_link",
            "color",
            "board",
            "counts",
            "created_at",
            "creator(id,url,first_name,last_name,username,image)",
            "image",
            "media",
            "metadata"
        };

        private static string[] BoardFields => new[]
        {
            "id",
            "url",
            "name",
            "created_at",
            "creator(id,url,first_name,last_name,username,image)",
            "counts",
            "description",
            "reason",
            "privacy",
            "image"
        };
    }
}
