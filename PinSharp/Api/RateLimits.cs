using System;

namespace PinSharp.Api
{
    public class RateLimits : IRateLimits
    {
        public RateLimits(int limit, int remaining, DateTimeOffset lastUpdated)
        {
            Limit = limit;
            Remaining = remaining;
            LastUpdated = lastUpdated;
        }

        public int Limit { get; }

        public int Remaining { get; }

        public DateTimeOffset LastUpdated { get; }
    }
}