using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    public interface IMeApi
    {
        /// <summary>
        /// Returns information about the user linked with the used access token.
        /// </summary>
        /// <returns></returns>
        Task<IDetailedUser> GetUserAsync();
        Task<IEnumerable<IUserBoard>> GetBoardsAsync();

        Task<PagedResponse<IUserPin>> GetPinsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<IUser>> GetFollowersAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<IBoard>> GetSuggestedBoardsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<IBoard>> GetSuggestedBoardsAsync(string pinId, string cursor = null, int limit = 0);
        Task<PagedResponse<IBoard>> GetFollowingBoardsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<IUser>> GetFollowingUsersAsync(string cursor = null, int limit = 0);
        Task<PagedResponse<IUserBoard>> SearchBoardsAsync(string query, string cursor = null, int limit = 0);
        Task<PagedResponse<IUserPin>> SearchPinsAsync(string query, string cursor = null, int limit = 0);

        Task FollowBoardAsync(string board);
        Task UnfollowBoardAsync(string board);
        Task FollowUserAsync(string user);
        Task UnfollowUserAsync(string user);
    }
}