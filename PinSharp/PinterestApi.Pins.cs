using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;
using PinSharp.Models.Responses;

namespace PinSharp
{
    public partial class PinterestApi : IPinsApi
    {
        public async Task<dynamic> GetPinAsync(string id, params string[] fields)
        {
            return await GetPinAsync(id, fields.AsEnumerable());
        }

        public async Task<dynamic> GetPinAsync(string id, IEnumerable<string> fields)
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/pins/{id}/", fields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<dynamic>();
            }
        }

        public async Task<Pin> GetPinAsync(string id)
        {
            var url = GetUrlWithFields($"{BaseUrl}/{ApiVersion}/pins/{id}/", PinFields);

            using (var client = new WebClient())
            {
                var response = await client.DownloadStringTaskAsync(url);
                var json = JsonConvert.DeserializeObject<JObject>(response);
                var data = json.SelectToken("data");
                return data.ToObject<Pin>();
            }
        }

        public async Task<CreatePinResponse> CreatePin(string board, string imageUrl, string note, string link = "")
        {
            if (!IsValidUrl(imageUrl))
                throw new ArgumentException($"'{imageUrl}' is not a valid URL", nameof(imageUrl));

            return await Post<object, CreatePinResponse>("pins", new {board, note, link, image_url = imageUrl});
        }

        public async Task<CreatePinResponse> CreatePinFromBase64(string board, string imageBase64, string note, string link = "")
        {
            if (!IsBase64String(imageBase64))
                throw new ArgumentException("The string is not valid base64", nameof(imageBase64));

            return await Post<object, CreatePinResponse>("pins", new { board, note, link, image_base64 = imageBase64 });
        }

        public async Task DeletePin(string id)
        {
            await Delete($"pins/{id}");
        }

        public async Task<CreatePinResponse> EditPin(string id, string board, string note, string link)
        {
            return await Patch<object, CreatePinResponse>($"pins/{id}", new { board, note, link });
        }

        private static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        private static bool IsValidUrl(string url)
        {
            Uri uri;
            return Uri.TryCreate(url, UriKind.Absolute, out uri);
        }
    }
}
