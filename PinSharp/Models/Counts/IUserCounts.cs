namespace PinSharp.Models.Counts
{
    public interface IUserCounts
    {
        int Boards { get; set; }
        int Followers { get; set; }
        int Following { get; set; }
        int Pins { get; set; }
    }
}