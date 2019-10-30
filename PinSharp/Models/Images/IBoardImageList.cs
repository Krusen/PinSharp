using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public interface IBoardImageList
    {
        [JsonProperty("60x60")]
        IImageInfo W60 { get; set; }
    }
}