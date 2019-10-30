namespace PinSharp.Models.Counts
{
    public interface IBoardCounts
    {
        int Collaborators { get; set; }
        int Followers { get; set; }
        int Pins { get; set; }
    }
}