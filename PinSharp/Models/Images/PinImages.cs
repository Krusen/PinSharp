using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class PinImages
    {
        [JsonProperty("70x")]
        public ImageInfo W70 { get; set; }

        [JsonProperty("236x")]
        public ImageInfo W236 { get; set; }

        [JsonProperty("736x")]
        public ImageInfo W736 { get; set; }

        public ImageInfo Original { get; set; }
    }
}
