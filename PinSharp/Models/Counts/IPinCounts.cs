using System;
using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Counts
{
    [JsonConverter(typeof(InterfaceConverter<Counts>))]
    public interface IPinCounts
    {
        int Comments { get; set; }
        int Saves { get; set; }

        [Obsolete("Use 'Saves' instead. This property will be removed in a future version")]
        int Repins { get; }
    }
}