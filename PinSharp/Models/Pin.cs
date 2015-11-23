using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public class Pin
    {
        public string Id { get; set; }

        public string Url { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public string Note { get; set; }

        public User Creator { get; set; }

        public Board Board { get; set; }

        public PinCounts Counts { get; set; }

        [JsonProperty("image")]
        public PinImages Images { get; set; }

        public string Link { get; set; }

        public string Color { get; set; }

        public IDictionary<string,string> Media { get; set; }

        public IDictionary<string, string> Attribution { get; set; }

        public IDictionary<string, object> Metadata { get; set; }
    }
}