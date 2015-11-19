using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

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

        public async Task CreatePin(string board, string note, string link, string imageUrl)
        {
            if (!IsValidUrl(imageUrl))
                throw new ArgumentException($"'{imageUrl}' is not a valid URL", nameof(imageUrl));

            throw new NotImplementedException();
        }

        public async Task CreatePinFromBase64(string board, string note, string link, string imageBase64)
        {
            if (!IsBase64String(imageBase64))
                throw new ArgumentException("The string is not valid base64", nameof(imageBase64));

            throw new NotImplementedException();
        }

        public async Task DeletePin(string id)
        {
            throw new NotImplementedException();
        }

        public async Task EditPin(string id, string board, string note, string link)
        {
            throw new NotImplementedException();
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
