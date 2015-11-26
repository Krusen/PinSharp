using System;

namespace PinSharp.Api
{
    [Flags]
    public enum Scopes
    {
        ReadPublic = 1 << 0,
        WritePublic = 1 << 1,
        ReadRelationships = 1 << 2,
        WriteRelationShips = 1 << 3,

        ReadAll = ReadPublic | ReadRelationships,
        WriteAll = WritePublic | WriteRelationShips,
        All = ReadAll | WriteAll,
    }
}
