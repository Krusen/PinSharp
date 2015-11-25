using System;

namespace PinSharp
{
    [Flags]
    public enum PinterestScopes
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
