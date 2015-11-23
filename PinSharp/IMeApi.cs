using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;
using PinSharp.Models.Responses;

namespace PinSharp
{
    public interface IMeApi
    {
        Task<UserDetails> GetUserAsync();
        Task<IEnumerable<UserBoard>> GetBoardsAsync();

        Task<PagedResponse<UserPin>> GetPinsAsync();
        Task<PagedResponse<UserPin>> GetPinsAsync(int limit);
        Task<PagedResponse<UserPin>> GetPinsAsync(string cursor);
        Task<PagedResponse<UserPin>> GetPinsAsync(string cursor, int limit);

        Task<PagedResponse<UserPin>> GetLikedPinsAsync();
        Task<PagedResponse<UserPin>> GetLikedPinsAsync(int limit);
        Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor);
        Task<PagedResponse<UserPin>> GetLikedPinsAsync(string cursor, int limit);

        Task<PagedResponse<User>> GetFollowersAsync();
        Task<PagedResponse<User>> GetFollowersAsync(int limit);
        Task<PagedResponse<User>> GetFollowersAsync(string cursor);
        Task<PagedResponse<User>> GetFollowersAsync(string cursor, int limit);

        Task<PagedResponse<Board>> GetSuggestedBoardsAsync();
        Task<PagedResponse<Board>> GetSuggestedBoardsAsync(int limit);
        Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor);
        Task<PagedResponse<Board>> GetSuggestedBoardsAsync(string cursor, int limit);

        Task<PagedResponse<Board>> GetFollowingBoardsAsync();
        Task<PagedResponse<Board>> GetFollowingBoardsAsync(int limit);
        Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor);
        Task<PagedResponse<Board>> GetFollowingBoardsAsync(string cursor, int limit);

        Task<PagedResponse<Interest>> GetFollowingInterestsAsync();
        Task<PagedResponse<Interest>> GetFollowingInterestsAsync(int limit);
        Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor);
        Task<PagedResponse<Interest>> GetFollowingInterestsAsync(string cursor, int limit);

        Task<PagedResponse<User>> GetFollowingUsersAsync();
        Task<PagedResponse<User>> GetFollowingUsersAsync(int limit);
        Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor);
        Task<PagedResponse<User>> GetFollowingUsersAsync(string cursor, int limit);

        Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query);
        Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, int limit);
        Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor);
        Task<PagedResponse<UserBoard>> SearchBoardsAsync(string query, string cursor, int limit);

        Task<PagedResponse<UserPin>> SearchPinsAsync(string query);
        Task<PagedResponse<UserPin>> SearchPinsAsync(string query, int limit);
        Task<PagedResponse<UserPin>> SearchPinsAsync(string query, string cursor);
        Task<PagedResponse<UserPin>> SearchPinsAsync(string query, string cursor, int limit);

        Task FollowBoardAsync(string board);
        Task UnfollowBoardAsync(string board);
        Task FollowUserAsync(string user);
        Task UnfollowUserAsync(string user);
    }
}