using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IBoardsApi, IMeApi, IPinsApi, IUsersApi
    {
        internal string AccessToken { get; set; }
        internal string ApiVersion { get; set; }

        internal PinterestApi(string accessToken, string apiVersion)
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;
        }

        protected string GetUrlWithFields(string url, IEnumerable<string> fields)
        {
            var fieldsString = string.Join(",", fields);
            return $"{url}?access_token={AccessToken}&fields={fieldsString}";
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
