using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Images
{
    [JsonConverter(typeof(InterfaceConverter<ImageList>))]
    public interface IBoardImageList
    {
        ImageInfo W60 { get; set; }
    }
}