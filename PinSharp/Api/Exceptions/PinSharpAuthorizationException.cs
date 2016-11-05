namespace PinSharp.Api.Exceptions
{
    public class PinSharpAuthorizationException : PinSharpException
    {
        public PinSharpAuthorizationException(string message)
            : base(message)
        {
            HttpStatusCode = 401;
        }
    }
}
