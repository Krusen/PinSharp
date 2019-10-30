using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;
using PinSharp.Serialization;

namespace PinSharp.Models
{
    [JsonConverter(typeof(InterfaceConverter<Pin>))]
    public interface IUserPin
    {
        string Id { get; set; }

        string Url { get; set; }

        DateTime CreatedAt { get; set; }

        string Note { get; set; }

        IBoard Board { get; set; }

        IPinCounts Counts { get; set; }

        IPinImageList Images { get; set; }

        string Link { get; set; }

        string OriginalLink { get; set; }

        string Color { get; set; }

        IDictionary<string, string> Media { get; set; }

        IDictionary<string, dynamic> Attribution { get; set; }

        IDictionary<string, dynamic> Metadata { get; set; }
    }

    [JsonConverter(typeof(InterfaceConverter<Pin>))]
    public interface IPin : IUserPin
    {
    }
}