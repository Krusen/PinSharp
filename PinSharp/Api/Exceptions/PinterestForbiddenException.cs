namespace PinSharp.Api.Exceptions
{
    public class PinterestForbiddenException : PinterestException
    {
        public PinterestForbiddenException(string message)
            : base(message)
        {
            HttpStatusCode = 403;
        }
    }
}
