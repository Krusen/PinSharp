using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinSharp.Models;
using PinSharp.Models.Responses;

namespace PinSharp
{
    public partial class PinterestApi : IMeApi
    {
        public async Task<UserDetails> GetUserAsync()
        {
            return await Get<UserDetails>("me", UserFields);
        }

        public async Task<IEnumerable<UserBoard>> GetBoardsAsync()
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            return await Get<IEnumerable<UserBoard>>("me/boards", fields);
        }


        public async Task<PagedResponse<UserPin>> GetPinsAsync()
        {
            return await (this as IMeApi).GetPinsAsync(null, 0);
        }

        public async Task<PagedResponse<UserPin>> GetPinsAsync(int limit)
        {
            return await (this as IMeApi).GetPinsAsync(null, limit);
        }

        async Task<PagedResponse<UserPin>> IMeApi.GetPinsAsync(string cursor)
        {
            return await (this as IMeApi).GetPinsAsync(cursor, 0);
        }

        async Task<PagedResponse<UserPin>> IMeApi.GetPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserPin>("me/pins", fields, cursor, limit);
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<UserPin>> GetLikedPinsAsync()
        {
            return await GetLikedPinsAsync(null, 0);
        }

        public async Task<PagedResponse<UserPin>> GetLikedPinsAsync(int limit)
        {
            return await GetLikedPinsAsync(null, limit);
        }

        public async Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor)
        {
            return await GetLikedPinsAsync(cursor, 0);
        }

        public async Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserPin>("me/likes", fields, cursor, limit);
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<User>> GetFollowersAsync()
        {
            return await GetFollowersAsync(null, 0);
        }

        public async Task<PagedResponse<User>> GetFollowersAsync(int limit)
        {
            return await GetFollowersAsync(null, limit);
        }

        public async Task<PagedResponse<User>> GetFollowersAsync(string cursor)
        {
            return await GetFollowersAsync(cursor, 0);
        }

        public async Task<PagedResponse<User>> GetFollowersAsync(string cursor, int limit)
        {
            var response = await GetPaged<User>("me/followers", cursor, limit);
            return new PagedResponse<User>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<Board>> GetSuggestedBoardsAsync()
        {
            return await GetSuggestedBoardsAsync(null, 0);
        }

        public async Task<PagedResponse<Board>> GetSuggestedBoardsAsync(int limit)
        {
            return await GetSuggestedBoardsAsync(null, limit);
        }

        public async Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor)
        {
            return await GetSuggestedBoardsAsync(cursor, 0);
        }

        public async Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor, int limit)
        {
            var response = await GetPaged<Board>("me/boards/suggested", BoardFields, cursor, limit);
            return new PagedResponse<Board>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<Board>> GetFollowingBoardsAsync()
        {
            return await GetFollowingBoardsAsync(null, 0);
        }

        public async Task<PagedResponse<Board>> GetFollowingBoardsAsync(int limit)
        {
            return await GetFollowingBoardsAsync(null, limit);
        }

        public async Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor)
        {
            return await GetFollowingBoardsAsync(cursor, 0);
        }

        public async Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor, int limit)
        {
            var response = await GetPaged<Board>("me/following/boards", BoardFields, cursor, limit);
            return new PagedResponse<Board>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync()
        {
            return await GetFollowingInterestsAsync(null, 0);
        }

        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync(int limit)
        {
            return await GetFollowingInterestsAsync(null, limit);
        }

        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor)
        {
            return await GetFollowingInterestsAsync(cursor, 0);
        }

        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor, int limit)
        {
            var response = await GetPaged<Interest>("me/following/interests", cursor, limit);
            return new PagedResponse<Interest>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<User>> GetFollowingUsersAsync()
        {
            return await GetFollowingUsersAsync(null, 0);
        }

        public async Task<PagedResponse<User>> GetFollowingUsersAsync(int limit)
        {
            return await GetFollowingUsersAsync(null, limit);
        }

        public async Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor)
        {
            return await GetFollowingUsersAsync(cursor, 0);
        }

        public async Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor, int limit)
        {
            var response = await GetPaged<User>("me/following/users", cursor, limit);
            return new PagedResponse<User>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query)
        {
            return await SearchBoardsAsync(query, null, 0);
        }

        public async Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, int limit)
        {
            return await SearchBoardsAsync(query, null, limit);
        }

        public async Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor)
        {
            return await SearchBoardsAsync(query, cursor, 0);
        }

        public async Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor, int limit)
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserBoard>($"me/search/boards/?query={query}", fields, cursor, limit);
            return new PagedResponse<UserBoard>(response.Data, response.Page?.Cursor);
        }


        public async Task<PagedResponse<UserPin>> SearchPinsAsync(string query)
        {
            return await SearchPinsAsync(query, null, 0);
        }

        public async Task<PagedResponse<UserPin>> SearchPinsAsync(string query, int limit)
        {
            return await SearchPinsAsync(query, null, limit);
        }

        public async Task<PagedResponse<UserPin>> SearchPinsAsync(string query, string cursor)
        {
            return await SearchPinsAsync(query, cursor, 0);
        }

        public async Task<PagedResponse<UserPin>> SearchPinsAsync(string query, string cursor, int limit)
        {
            var fields = UserFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserPin>($"me/search/pins?query={query}", fields, cursor, limit);
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }


        public async Task FollowBoardAsync(string board)
        {
            await Post("me/following/boards", new {board});
        }

        public async Task UnfollowBoardAsync(string board)
        {
            await Delete($"me/following/boards/{board}");
        }

        public async Task FollowUserAsync(string user)
        {
            await Post("me/following/users", new {user});
        }

        public async Task UnfollowUserAsync(string user)
        {
            await Delete($"me/following/users/{user}");
        }
    }
}
