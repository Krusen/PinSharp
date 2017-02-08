using System;

namespace PinSharp.Api.Exceptions
{
    public class PinSharpTimeoutException : PinSharpException
    {
        public PinSharpTimeoutException()
        {
            HttpStatusCode = 408;
        }

        public PinSharpTimeoutException(string message)
            : base(message)
        {
            HttpStatusCode = 408;
        }

        public PinSharpTimeoutException(string message, Exception inner)
            : base(message, inner)
        {
            HttpStatusCode = 408;
        }
    }
}
