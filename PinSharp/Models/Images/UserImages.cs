using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class UserImages
    {
        [JsonProperty("60x")]
        public ImageInfo W60 { get; set; }

        [JsonProperty("110x")]
        public ImageInfo W110 { get; set; }

        [JsonProperty("165x")]
        public ImageInfo W165 { get; set; }

        [JsonProperty("280x")]
        public ImageInfo W280 { get; set; }
    }
}
