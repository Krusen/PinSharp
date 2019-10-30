using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public interface IDetailedBoard : IUserBoard
    {
        // TODO: Verify actually IDetailedUser and not just IUser
        IDetailedUser Creator { get; set; }
    }

    public interface IUserBoard : IBoard
    {
        DateTime CreatedAt { get; set; }

        string Description { get; set; }

        IBoardCounts Counts { get; set; }

        [JsonProperty("image")]
        IBoardImageList Images { get; set; }

        string Reason { get; set; }

        string Privacy { get; set; }
    }

    public interface IBoard
    {
        string Id { get; set; }

        string Url { get; set; }

        string Name { get; set; }
    }
}