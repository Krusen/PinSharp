using System;

namespace PinSharp.Api.Exceptions
{
    public class PinterestTimeoutException : PinterestException
    {
        public PinterestTimeoutException()
        {
            HttpStatusCode = 408;
        }

        public PinterestTimeoutException(string message)
            : base(message)
        {
            HttpStatusCode = 408;
        }

        public PinterestTimeoutException(string message, Exception inner)
            : base(message, inner)
        {
            HttpStatusCode = 408;
        }
    }
}
