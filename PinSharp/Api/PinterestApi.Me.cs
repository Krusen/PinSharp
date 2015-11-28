using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    public partial class PinterestApi : IMeApi
    {
        public Task<UserDetails> GetUserAsync()
        {
            return GetAsync<UserDetails>("me", UserFields);
        }

        public Task<IEnumerable<UserBoard>> GetBoardsAsync()
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            return GetAsync<IEnumerable<UserBoard>>("me/boards", fields);
        }

        async Task<PagedResponse<UserPin>> IMeApi.GetPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<UserPin>("me/pins", fields, cursor, limit).Configured();
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<UserPin>("me/likes", fields, cursor, limit).Configured();
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<User>> GetFollowersAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<User>("me/followers", cursor, limit).Configured();
            return new PagedResponse<User>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<Board>("me/boards/suggested", BoardFields, cursor, limit).Configured();
            return new PagedResponse<Board>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<Board>("me/following/boards", BoardFields, cursor, limit).Configured();
            return new PagedResponse<Board>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<Interest>("me/following/interests", cursor, limit).Configured();
            return new PagedResponse<Interest>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<User>("me/following/users", cursor, limit).Configured();
            return new PagedResponse<User>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor, int limit)
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<UserBoard>($"me/search/boards/?query={query}", fields, cursor, limit).Configured();
            return new PagedResponse<UserBoard>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<UserPin>> SearchPinsAsync(string query, string cursor, int limit)
        {
            var fields = UserFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<UserPin>($"me/search/pins?query={query}", fields, cursor, limit).Configured();
            return new PagedResponse<UserPin>(response.Data, response.Page?.Cursor);
        }

        public Task FollowBoardAsync(string board)
        {
            return PostAsync("me/following/boards", new {board});
        }

        public Task UnfollowBoardAsync(string board)
        {
            return DeleteAsync($"me/following/boards/{board}");
        }

        public Task FollowUserAsync(string user)
        {
            return PostAsync("me/following/users", new {user});
        }

        public Task UnfollowUserAsync(string user)
        {
            return DeleteAsync($"me/following/users/{user}");
        }
    }
}
