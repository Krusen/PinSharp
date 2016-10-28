using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Images
{
    [JsonConverter(typeof(InterfaceConverter<ImageList>))]
    public interface IPinImageList
    {
        ImageInfo Original { get; set; }
    }
}