using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public class Pin : IPin, IUserPin
    {
        public string Id { get; set; }

        public string Url { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public string Note { get; set; }

        public User Creator { get; set; }

        public IBoard Board { get; set; }

        public IPinCounts Counts { get; set; }

        [JsonProperty("image")]
        public IPinImageList Images { get; set; }

        public string Link { get; set; }

        [JsonProperty("original_link")]
        public string OriginalLink { get; set; }

        public string Color { get; set; }

        public IDictionary<string, string> Media { get; set; }

        public IDictionary<string, dynamic> Attribution { get; set; }

        public IDictionary<string, dynamic> Metadata { get; set; }
    }
}