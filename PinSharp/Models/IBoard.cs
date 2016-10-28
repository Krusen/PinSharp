using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;
using PinSharp.Serialization;

namespace PinSharp.Models
{
    [JsonConverter(typeof(InterfaceConverter<Board>))]
    public interface IDetailedBoard : IUserBoard
    {
        User Creator { get; set; }
    }

    [JsonConverter(typeof(InterfaceConverter<Board>))]
    public interface IUserBoard : IBoard
    {
        //[JsonProperty("created_at")]
        DateTime CreatedAt { get; set; }

        string Description { get; set; }

        IBoardCounts Counts { get; set; }

        //[JsonProperty("image")]
        IBoardImageList Images { get; set; }

        string Reason { get; set; }

        string Privacy { get; set; }
    }

    [JsonConverter(typeof(InterfaceConverter<Board>))]
    public interface IBoard
    {
        string Id { get; set; }

        string Url { get; set; }

        string Name { get; set; }
    }
}