namespace PinSharp.Api.Exceptions
{
    public class PinSharpNotFoundException : PinSharpException
    {
        public PinSharpNotFoundException(string message)
            : base(message)
        {
            HttpStatusCode = 404;
        }
    }
}
