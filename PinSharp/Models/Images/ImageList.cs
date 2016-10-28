using Newtonsoft.Json;

namespace PinSharp.Models.Images
{
    public class ImageList : IPinImageList, IUserImageList, IBoardImageList
    {
        public ImageInfo Original { get; set; }

        [JsonProperty("60x60")]
        public ImageInfo W60 { get; set; }
    }
}
