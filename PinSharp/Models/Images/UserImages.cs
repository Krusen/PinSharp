using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class UserImages
    {
        [JsonProperty("60x")]
        public PinterestImage W60 { get; set; }

        [JsonProperty("110x")]
        public PinterestImage W110 { get; set; }

        [JsonProperty("165x")]
        public PinterestImage W165 { get; set; }

        [JsonProperty("280x")]
        public PinterestImage W280 { get; set; }
    }
}
