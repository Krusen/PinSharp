using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            return await Get<dynamic>($"pins/{id}", fields);
        }

        public async Task<Pin> GetPinAsync(string id)
        {
            return await Get<Pin>($"pins/{id}", PinFields);
        }

        public async Task<CreatePinResponse> CreatePinAsync(string board, string imageUrl, string note, string link = "")
        {
            if (!IsValidUrl(imageUrl))
                throw new ArgumentException($"'{imageUrl}' is not a valid URL", nameof(imageUrl));

            return await Post<CreatePinResponse>("pins", new {board, note, link, image_url = imageUrl});
        }

        public async Task<CreatePinResponse> CreatePinFromBase64Async(string board, string imageBase64, string note, string link = "")
        {
            if (!IsBase64String(imageBase64))
                throw new ArgumentException("The string is not valid base64", nameof(imageBase64));

            return await Post<CreatePinResponse>("pins", new { board, note, link, image_base64 = imageBase64 });
        }

        public async Task DeletePinAsync(string id)
        {
            await Delete($"pins/{id}");
        }

        public async Task<CreatePinResponse> UpdatePinAsync(string id, string board, string note, string link)
        {
            return await Patch<CreatePinResponse>($"pins/{id}", new { board, note, link });
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
