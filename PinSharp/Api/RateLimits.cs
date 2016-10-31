using System;

namespace PinSharp.Api
{
    public class RateLimits : IRateLimits
    {
        public int Limit { get; internal set; }
        public int Remaining { get; internal set; }
        public DateTimeOffset LastUpdated { get; internal set; }
    }
}