namespace PinSharp.Models.Images
{
    public interface IImageInfo
    {
        string Url { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}
