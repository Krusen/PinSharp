using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Counts
{
    [JsonConverter(typeof(InterfaceConverter<Counts>))]
    public interface IUserCounts
    {
        int Boards { get; set; }
        int Followers { get; set; }
        int Following { get; set; }
        int Pins { get; set; }
    }
}