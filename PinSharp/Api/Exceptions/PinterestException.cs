using System;

namespace PinSharp.Api.Exceptions
{
    public class PinterestException : Exception
    {
        public int? HttpStatusCode { get; internal set; }

        public string RequestUrl { get; internal set; }

        public string ResponseContent { get; internal set; }

        public PinterestException()
        {

        }

        public PinterestException(string message)
            : base(message)
        {

        }

        public PinterestException(string message, Exception inner)
            : base(message, inner)
        {

        }

        internal static T Create<T>(string message, string requestUrl, string responseContent, int? httpStatusCode = null)
            where T : PinterestException
        {
            var exception = (T) Activator.CreateInstance(typeof (T), message);
            exception.RequestUrl = requestUrl;
            exception.ResponseContent = responseContent;
            exception.HttpStatusCode = httpStatusCode;
            return exception;
        }
    }
}
