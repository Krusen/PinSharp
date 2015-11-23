using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class PinImages
    {
        [JsonProperty("70x")]
        public PinterestImage W70 { get; set; }

        [JsonProperty("236x")]
        public PinterestImage W236 { get; set; }

        [JsonProperty("736x")]
        public PinterestImage W736 { get; set; }

        public PinterestImage Original { get; set; }
    }
}
