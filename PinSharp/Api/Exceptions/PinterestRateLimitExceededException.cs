using System;

namespace PinSharp.Api.Exceptions
{
    public class PinterestRateLimitExceededException : PinterestException
    {
        public PinterestRateLimitExceededException()
        {
            HttpStatusCode = 429;
        }

        public PinterestRateLimitExceededException(string message)
            : base(message)
        {
            HttpStatusCode = 429;
        }

        public PinterestRateLimitExceededException(string message, Exception inner)
            : base(message, inner)
        {
            HttpStatusCode = 429;
        }
    }
}
