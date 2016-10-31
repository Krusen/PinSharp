using System;

namespace PinSharp.Api
{
    public interface IRateLimits
    {
        int Limit { get; }
        int Remaining { get; }
        DateTimeOffset LastUpdated { get; }
    }
}