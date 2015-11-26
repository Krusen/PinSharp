using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    public interface IMeApi
    {
        Task<UserDetails> GetUserAsync();
        Task<IEnumerable<UserBoard>> GetBoardsAsync();

        Task<PagedResponse<UserPin>> GetPinsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<User>> GetFollowersAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor = null, int limit = 0);
        Task<PagedResponse<UserPin>> SearchPinsAsync(string query, string cursor = null, int limit = 0);

        Task FollowBoardAsync(string board);
        Task UnfollowBoardAsync(string board);
        Task FollowUserAsync(string user);
        Task UnfollowUserAsync(string user);
    }
}