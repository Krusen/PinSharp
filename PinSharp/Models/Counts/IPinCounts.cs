using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Counts
{
    [JsonConverter(typeof(InterfaceConverter<Counts>))]
    public interface IPinCounts
    {
        int Comments { get; set; }
        int Likes { get; set; }
        int Repins { get; set; }
    }
}