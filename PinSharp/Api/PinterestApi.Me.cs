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
            return GetAsync<IEnumerable<IUserBoard>>("me/boards", new RequestOptions(BoardFields));
        }

        Task<PagedResponse<IUserPin>> IMeApi.GetPinsAsync(string cursor, int limit)
        {
            var responseTask = GetPagedAsync<IUserPin>("me/pins", new RequestOptions(PinFields, cursor, limit));
            return PagedResponse<IUserPin>.FromTask(responseTask);
        }

        public Task<PagedResponse<IUser>> GetFollowersAsync(string cursor, int limit)
        {
            var responseTask = GetPagedAsync<IUser>("me/followers", new RequestOptions(cursor, limit));
            return PagedResponse<IUser>.FromTask(responseTask);
        }

        public Task<PagedResponse<IBoard>> GetSuggestedBoardsAsync(string pin, string cursor, int limit)
        {
            // NOTE: This endpoint uses 'count' instead of 'limit' for some reason
            var responseTask = GetPagedAsync<IBoard>("me/boards/suggested", new RequestOptions(BoardFields, cursor, new {count = limit}));
            return PagedResponse<IBoard>.FromTask(responseTask);
        }

        public Task<PagedResponse<IBoard>> GetSuggestedBoardsAsync(string cursor, int limit)
        {
            // NOTE: This endpoint uses 'count' instead of 'limit' for some reason
            var responseTask = GetPagedAsync<IBoard>("me/boards/suggested", new RequestOptions(BoardFields, cursor, new {count = limit}));
            return PagedResponse<IBoard>.FromTask(responseTask);
        }

        public Task<PagedResponse<IBoard>> GetFollowingBoardsAsync(string cursor, int limit)
        {
            var responseTask = GetPagedAsync<IBoard>("me/following/boards", new RequestOptions(BoardFields, cursor, limit));
            return PagedResponse<IBoard>.FromTask(responseTask);
        }

        public Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor, int limit)
        {
            var responseTask = GetPagedAsync<Interest>("me/following/interests", new RequestOptions( cursor, limit));
            return PagedResponse<Interest>.FromTask(responseTask);
        }

        public Task<PagedResponse<IUser>> GetFollowingUsersAsync(string cursor, int limit)
        {
            var responseTask = GetPagedAsync<IUser>("me/following/users", new RequestOptions(cursor, limit));
            return PagedResponse<IUser>.FromTask(responseTask);
        }

        public Task<PagedResponse<IUserBoard>> SearchBoardsAsync(string query, string cursor, int limit)
        {
            var responseTask = GetPagedAsync<IUserBoard>($"me/search/boards", new RequestOptions(query, BoardFields, cursor, limit));
            return PagedResponse<IUserBoard>.FromTask(responseTask);
        }

        public Task<PagedResponse<IUserPin>> SearchPinsAsync(string query, string cursor, int limit)
        {
            var responseTask = GetPagedAsync<IUserPin>($"me/search/pins", new RequestOptions(query, PinFields, cursor, limit));
            return PagedResponse<IUserPin>.FromTask(responseTask);
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
