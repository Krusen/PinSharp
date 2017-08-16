# PinSharp

[![license](https://img.shields.io/badge/license-Unlicense-blue.svg)](https://github.com/Krusen/PinSharp/blob/master/LICENSE.MD)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/to2o4ik0nw5d98js/branch/master?svg=true)](https://ci.appveyor.com/project/Krusen/pinsharp)
[![Coverage](https://coveralls.io/repos/github/Krusen/PinSharp/badge.svg?branch=master)](https://coveralls.io/github/Krusen/PinSharp?branch=master)
[![CodeFactor](https://www.codefactor.io/repository/github/krusen/pinsharp/badge)](https://www.codefactor.io/repository/github/krusen/pinsharp)
[![NuGet](https://buildstats.info/nuget/pinsharp?includePreReleases=false)](https://www.nuget.org/packages/PinSharp)
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bhttps%3A%2F%2Fgithub.com%2FKrusen%2FPinSharp.svg?type=shield)](https://app.fossa.io/projects/git%2Bhttps%3A%2F%2Fgithub.com%2FKrusen%2FPinSharp?ref=badge_shield)

An async C# wrapper library for the Pinterest API.

- https://developers.pinterest.com/docs/getting-started/introduction/


# Notice
I'm not maintaining this regularly as I don't use it that much.
If you have any issues or request please create a new issue and I'll have a look.


# Overview

- [New in version 2.0](#new-in-version-20)
  - [Breaking changes](#breaking-changes-in-version-20)
- [Examples](#examples)


# New in version 2.0

A lot of the changes are code cleanup and refactoring, but there is a few new features.

### Rate limit information

Information about rate limits are now stored on the `PinSharpClient` in the property `RateLimits`

It contains information about the request limit and remaining requests and when this information was last updated
(i.e. the time of your last request through this client).

### Exceptions

The client now throws its own exceptions all extending from `PinSharpException`.

For example a `PinSharpRateLimitExceededException` will be thrown if the rate limit has been exceeded.


## Breaking changes in version 2.0

All return types have generally been changed from a concrete class to an interface, e.g. `Pin` to `IPin`.
`BoardDetails` and `UserDetails` have also been renamed in the process to `IDetailedBoard` and `IDetailedUsers`.

### Renamed

- `PinterestClient` renamed to `PinSharpClient`
- `PinterestAuthClient` renamed to `PinSharpAuthClient`
- `PinterestApi` made `internal`
- `Scopes.WriteRelationShips` renamed to `Scopes.WriteRelationsships`

### Moved

- `PinSharp.IHttpClient` moved to `PinSharp.Http.IHttpClient`
- `PinSharp.Models.ImageInfo` moved to `PinSharp.Models.Images.ImageInfo`

### Refactored/combined

- Models
  - `BoardDetails` removed - merged into `Board` and exposed as `IDetaildBoard` interface
  - `UserDetails` removed - merged into `User` and exposed as `IDetailedUser` interface
  - `UserBoard` removed - merged into `Board` and exposed as `IUserBoard` interface
  - `UserPin` removed - merged into `Pin` and exposed as `IUserPin` interface
- Counts
  - `BoardCounts` removed - merged into **new** `Counts` and exposed as `IBoardCounts` interface
  - `PinCounts` removed - merged into **new** `Counts` and exposed as `IPinCounts` interface
  - `UserCounts` removed - merged into **new** `Counts` and exposed as `IUserCounts` interface
- Images
  - `BoardImages` removed - merged into **new** `ImageList` and exposed as `IBoardImageList` interface
  - `PinImages` removed - merged into **new** `ImageList` and exposed as `IPinImageList` interface
  - `UserImages` removed - merged into **new** `ImageList` and exposed as `IUserImageList` interface


# Examples

You need an access token to use the API.

If you don't have one already you can generate one here: https://developers.pinterest.com/tools/access_token/

```C#
// Create a client with your access token
var client = new PinSharpClient("AB_IBS7Q0fFQbXJ90JGtSDXNMV-tEBkfLftbK6JCpEWkGoA_MwAAAAA");

// Get board information
var board = await client.Boards.GetBoardAsync("machineshopcafe/best-of-mclaren-machine");

// Get pins on board
var pins = await client.Boards.GetPinsAsync("machineshopcafe/best-of-mclaren-machine");

// Get pins on board but only with fields 'creator' and 'board' as dynamic or your own type
var pins = await client.Boards.GetPinsAsync<dynamic>("rice_up/tableware", new[] { "creator", "board" });

// Get user info of the user associated with the access token
var user = await client.Me.GetUserAsync();

// Get pins of the user associated with the access token
var pins = await client.Me.GetPinsAsync();

// Get boards of the user associated with the access token
var boards = await client.Me.GetBoardsAsync();

// Search the associated user's pins or boards
var pins = await client.Me.SearchPinsAsync("mclaren");
var boards = await client.Me.SearchBoardsAsync("mclaren");

// Create new pin
var newPin = await client.Pins.CreatePinAsync("machineshopcafe/best-of-mclaren-machine", "http://i.imgur.com/abcdef.jpg", "Looks so cool!");

// Follow/unfollow board or user
await client.Me.FollowBoardAsync("machineshopcafe/best-of-mclaren-machine");
await client.Me.UnfollowBoardAsync("machineshopcafe/best-of-mclaren-machine");
await client.Me.FollowUserAsync("machineshopcafe");
await client.Me.UnfollowUserAsync("machineshopcafe");
```


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bhttps%3A%2F%2Fgithub.com%2FKrusen%2FPinSharp.svg?type=large)](https://app.fossa.io/projects/git%2Bhttps%3A%2F%2Fgithub.com%2FKrusen%2FPinSharp?ref=badge_large)