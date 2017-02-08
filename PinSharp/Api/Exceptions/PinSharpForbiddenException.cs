namespace PinSharp.Api.Exceptions
{
    public class PinSharpForbiddenException : PinSharpException
    {
        public PinSharpForbiddenException(string message)
            : base(message)
        {
            HttpStatusCode = 403;
        }
    }
}
