using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public interface IUserPin
    {
        string Id { get; set; }

        string Url { get; set; }

        [JsonProperty("created_at")]
        DateTime CreatedAt { get; set; }

        string Note { get; set; }

        IBoard Board { get; set; }

        IPinCounts Counts { get; set; }

        [JsonProperty("image")]
        IPinImageList Images { get; set; }

        string Link { get; set; }

        [JsonProperty("original_link")]
        string OriginalLink { get; set; }

        string Color { get; set; }

        IDictionary<string, string> Media { get; set; }

        IDictionary<string, dynamic> Attribution { get; set; }

        IDictionary<string, JObject> Metadata { get; set; }
    }

    public interface IPin : IUserPin
    {
        IUser Creator { get; set; }
    }
}