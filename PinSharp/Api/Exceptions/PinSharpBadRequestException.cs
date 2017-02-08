using System;

namespace PinSharp.Api.Exceptions
{
    public class PinSharpBadRequestException : PinSharpException
    {
        public PinSharpBadRequestException(string message)
            : base(message)
        {
            HttpStatusCode = 400;
        }
    }
}
