using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Counts
{
    [JsonConverter(typeof(InterfaceConverter<Counts>))]
    public interface IBoardCounts
    {
        int Collaborators { get; set; }
        int Followers { get; set; }
        int Pins { get; set; }
    }
}