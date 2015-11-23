namespace PinSharp.Models.Responses
{
    public class PagedApiResponse<T>
    {
        public T Data { get; set; }
        public PagingInfo Page { get; set; }
    }

    public class PagingInfo
    {
        public string Cursor { get; set; }
        public string Next { get; set; }
    }
}
