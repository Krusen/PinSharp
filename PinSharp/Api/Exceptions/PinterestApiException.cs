using System;

namespace PinSharp.Api.Exceptions
{
    public class PinterestApiException : Exception
    {
        public string Param { get; set; }
        public string Type { get; set; }

        public PinterestApiException(string message) : base(message)
        {

        }
    }
}
