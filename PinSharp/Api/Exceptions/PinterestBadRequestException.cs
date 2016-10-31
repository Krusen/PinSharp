using System;

namespace PinSharp.Api.Exceptions
{
    public class PinterestBadRequestException : PinterestException
    {
        public PinterestBadRequestException(string message)
            : base(message)
        {
            HttpStatusCode = 400;
        }
    }
}
