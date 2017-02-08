using System;

namespace PinSharp.Api
{
    // TODO: Implement saving these with the access token and/or the PinSharpClient
    /// <summary>
    /// The different permission scopes you can request access when requesting an access token.
    /// </summary>
    [Flags]
    public enum Scopes
    {
        // TODO: Implement this in PinSharpAuthClient
        /// <summary>
        /// Use GET method on a user’s profile, board and pin details, and the pins on a board.
        /// </summary>
        None = 0 << 0,

        /// <summary>
        /// Use GET method on a user’s pins, boards and likes.
        /// </summary>
        ReadPublic = 1 << 0,

        /// <summary>
        /// Use PATCH, POST and DELETE methods on a user’s pins and boards.
        /// </summary>
        WritePublic = 1 << 1,

        /// <summary>
        /// Use GET method on a user’s follows and followers (on boards, users and interests).
        /// </summary>
        ReadRelationships = 1 << 2,

        /// <summary>
        /// Use PATCH, POST and DELETE methods on a user’s follows and followers (on boards, users and interests).
        /// </summary>
        WriteRelationships = 1 << 3,

        /// <summary>
        /// Combination of <see cref="ReadPublic"/> and <see cref="ReadRelationships"/>.
        /// </summary>
        ReadAll = ReadPublic | ReadRelationships,

        /// <summary>
        /// Combination of <see cref="WritePublic"/> and <see cref="WriteRelationships"/>.
        /// </summary>
        WriteAll = WritePublic | WriteRelationships,

        /// <summary>
        /// Combination of all scopes - <see cref="ReadPublic"/>, <see cref="ReadRelationships"/>, <see cref="WritePublic"/> and <see cref="WriteRelationships"/>.
        /// </summary>
        All = ReadAll | WriteAll,
    }
}
