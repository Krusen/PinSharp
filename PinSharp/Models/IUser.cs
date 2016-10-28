using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;
using PinSharp.Serialization;

namespace PinSharp.Models
{
    [JsonConverter(typeof(InterfaceConverter<User>))]
    public interface IUser
    {
        string Id { get; set; }

        string Url { get; set; }

        //[JsonProperty("first_name")]
        string FirstName { get; set; }

        //[JsonProperty("last_name")]
        string LastName { get; set; }

        string UserName { get; set; }

        //[JsonProperty("image")]
        IUserImageList Images { get; set; }
    }

    [JsonConverter(typeof(InterfaceConverter<User>))]
    public interface IDetailedUser : IUser
    {

        //[JsonProperty("account_type")]
        string AccountType { get; set; }

        string Bio { get; set; }

        //[JsonProperty("created_at")]
        DateTime CreatedAt { get; set; }

        UserCounts Counts { get; set; }
    }
}