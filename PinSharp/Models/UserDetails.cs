using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;

namespace PinSharp.Models
{
    public class UserDetails : User
    {
        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        public string Bio { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public UserCounts Counts { get; set; }
    }
}
