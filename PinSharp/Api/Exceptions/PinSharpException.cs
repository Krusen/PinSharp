﻿using System;

namespace PinSharp.Api.Exceptions
{
    public class PinSharpException : Exception
    {
        public int? HttpStatusCode { get; internal set; }

        public string RequestUrl { get; internal set; }

        public string ResponseContent { get; internal set; }

        public PinSharpException()
        {

        }

        public PinSharpException(string message)
            : base(message)
        {

        }

        public PinSharpException(string message, Exception inner)
            : base(message, inner)
        {

        }

        internal static T Create<T>(string message, string requestUrl, string responseContent, int? httpStatusCode = null)
            where T : PinSharpException
        {
            var exception = (T) Activator.CreateInstance(typeof (T), message);
            exception.RequestUrl = requestUrl;
            exception.ResponseContent = responseContent;

            if (httpStatusCode != null)
                exception.HttpStatusCode = httpStatusCode;

            return exception;
        }
    }
}
