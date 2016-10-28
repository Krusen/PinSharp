using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public class User : IDetailedUser, IUser
    {
        public string Id { get; set; }

        public string Url { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [JsonProperty("image")]
        public IUserImageList Images { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        public string Bio { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public UserCounts Counts { get; set; }
    }
}
