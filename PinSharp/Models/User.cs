using System.Collections.Generic;
using Newtonsoft.Json;

namespace PinSharp.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Url { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        #region Extra

        public string UserName { get; set; }

        [JsonProperty("image")]
        public IDictionary<string, PinterestImage> Images { get; set; }

        #endregion
    }
}
