using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class UserImages
    {
        [JsonProperty("60x60")]
        public ImageInfo W60 { get; set; }

        [JsonProperty("110x110")]
        public ImageInfo W110 { get; set; }

        [JsonProperty("165x165")]
        public ImageInfo W165 { get; set; }

        [JsonProperty("280x280")]
        public ImageInfo W280 { get; set; }
    }
}
