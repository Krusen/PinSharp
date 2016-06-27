using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class UserImages
    {
        [JsonProperty("60x60")]
        public ImageInfo W60 { get; set; }
    }
}
