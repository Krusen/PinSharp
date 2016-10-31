using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    internal partial class PinterestApi : IMeApi
    {
        public Task<IDetailedUser> GetUserAsync()
        {
            return GetAsync<IDetailedUser>("me", new RequestOptions(UserFields));
        }

        public Task<IEnumerable<IUserBoard>> GetBoardsAsync()
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            return GetAsync<IEnumerable<IUserBoard>>("me/boards", new RequestOptions(fields));
        }

        async Task<PagedResponse<IUserPin>> IMeApi.GetPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<IUserPin>("me/pins", new RequestOptions(fields, cursor, limit)).Configured();
            return new PagedResponse<IUserPin>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IUserPin>> GetLikedPinsAsync(string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<IUserPin>("me/likes", new RequestOptions(fields, cursor, limit)).Configured();
            return new PagedResponse<IUserPin>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IUser>> GetFollowersAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<IUser>("me/followers", new RequestOptions(cursor, limit)).Configured();
            return new PagedResponse<IUser>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IBoard>> GetSuggestedBoardsAsync(string pin, string cursor, int limit)
        {
            // NOTE: This endpoint uses 'count' instead of 'limit' for some reason
            var response = await GetPagedAsync<IBoard>("me/boards/suggested", new RequestOptions(BoardFields, cursor, new { count = limit })).Configured();
            return new PagedResponse<IBoard>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IBoard>> GetSuggestedBoardsAsync(string cursor, int limit)
        {
            // NOTE: This endpoint uses 'count' instead of 'limit' for some reason
            var response = await GetPagedAsync<IBoard>("me/boards/suggested", new RequestOptions(BoardFields, cursor, new { count = limit })).Configured();
            return new PagedResponse<IBoard>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IBoard>> GetFollowingBoardsAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<IBoard>("me/following/boards", new RequestOptions(BoardFields, cursor, limit)).Configured();
            return new PagedResponse<IBoard>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<Interest>("me/following/interests", new RequestOptions( cursor, limit)).Configured();
            return new PagedResponse<Interest>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IUser>> GetFollowingUsersAsync(string cursor, int limit)
        {
            var response = await GetPagedAsync<User>("me/following/users", new RequestOptions(cursor, limit)).Configured();
            return new PagedResponse<IUser>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IUserBoard>> SearchBoardsAsync(string query, string cursor, int limit)
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<IUserBoard>($"me/search/boards", new RequestOptions(query, fields, cursor, limit)).Configured();
            return new PagedResponse<IUserBoard>(response.Data, response.Page?.Cursor);
        }

        public async Task<PagedResponse<IUserPin>> SearchPinsAsync(string query, string cursor, int limit)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            var response = await GetPagedAsync<IUserPin>($"me/search/pins", new RequestOptions(query, fields, cursor, limit)).Configured();
            return new PagedResponse<IUserPin>(response.Data, response.Page?.Cursor);
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
