using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public interface IUser
    {
        string Id { get; set; }

        string Url { get; set; }

        [JsonProperty("first_name")]
        string FirstName { get; set; }

        [JsonProperty("last_name")]
        string LastName { get; set; }

        string UserName { get; set; }

        [JsonProperty("image")]
        IUserImageList Images { get; set; }
    }

    public interface IDetailedUser : IUser
    {
        [JsonProperty("account_type")]
        string AccountType { get; set; }

        string Bio { get; set; }

        [JsonProperty("created_at")]
        DateTime CreatedAt { get; set; }

        IUserCounts Counts { get; set; }
    }
}