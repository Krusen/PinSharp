using System;

namespace PinSharp.Api.Exceptions
{
    public class PinSharpRateLimitExceededException : PinSharpException
    {
        public PinSharpRateLimitExceededException()
        {
            HttpStatusCode = 429;
        }

        public PinSharpRateLimitExceededException(string message)
            : base(message)
        {
            HttpStatusCode = 429;
        }

        public PinSharpRateLimitExceededException(string message, Exception inner)
            : base(message, inner)
        {
            HttpStatusCode = 429;
        }
    }
}
