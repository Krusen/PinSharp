namespace PinSharp.Api.Responses
{
    internal class PagedApiResponse<T>
    {
        public T Data { get; set; }
        public PagingInfo Page { get; set; }
    }

    internal class PagingInfo
    {
        public string Cursor { get; set; }
        public string Next { get; set; }
    }
}
