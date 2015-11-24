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

        async Task<PagedResponse<UserPin>> IMeApi.GetPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserPin>("me/pins", fields, cursor, limit);
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserPin>("me/likes", fields, cursor, limit);
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<User>> GetFollowersAsync(string cursor, int limit)
        {
            var response = await GetPaged<User>("me/followers", cursor, limit);
            return new PagedResponse<User>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor, int limit)
        {
            var response = await GetPaged<Board>("me/boards/suggested", BoardFields, cursor, limit);
            return new PagedResponse<Board>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor, int limit)
        {
            var response = await GetPaged<Board>("me/following/boards", BoardFields, cursor, limit);
            return new PagedResponse<Board>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor, int limit)
        {
            var response = await GetPaged<Interest>("me/following/interests", cursor, limit);
            return new PagedResponse<Interest>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor, int limit)
        {
            var response = await GetPaged<User>("me/following/users", cursor, limit);
            return new PagedResponse<User>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor, int limit)
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPaged<UserBoard>($"me/search/boards/?query={query}", fields, cursor, limit);
            return new PagedResponse<UserBoard>(response.Data, response.Page?.Cursor);
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
            await DeleteAsync($"me/following/boards/{board}");
        }

        public async Task FollowUserAsync(string user)
        {
            await Post("me/following/users", new {user});
        }

        public async Task UnfollowUserAsync(string user)
        {
            await DeleteAsync($"me/following/users/{user}");
        }
    }
}
