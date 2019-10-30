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
    }

    [JsonConverter(typeof(InterfaceConverter<Board>))]
    public interface IUserBoard : IBoard
    {
        DateTime CreatedAt { get; set; }

        string Description { get; set; }

        IBoardCounts Counts { get; set; }

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