using Newtonsoft.Json;
using PinSharp.Serialization;

namespace PinSharp.Models.Images
{
    [JsonConverter(typeof(InterfaceConverter<ImageList>))]
    public interface IUserImageList
    {
        ImageInfo W60 { get; set; }
    }
}