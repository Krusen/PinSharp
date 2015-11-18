using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public interface IMeApi
    {
        Task<UserDetails> GetUserAsync();
        Task<IEnumerable<UserBoard>> GetBoardsAsync();
        Task<IEnumerable<UserPin>> GetPinsAsync(int limit = 0, string cursor = null);
        Task<IEnumerable<UserPin>> GetLikedPinsAsync(int limit = 0, string cursor = null);
        Task<IEnumerable<User>>  GetFollowersAsync(string cursor = null);
        Task<IEnumerable<Board>>  GetSuggestedBoardsAsync(string cursor = null);
        Task<IEnumerable<Board>> GetFollowingBoardsAsync(string cursor = null);
        Task<IEnumerable<Interest>> GetFollowingInterestsAsync(string cursor = null);
        Task<IEnumerable<User>> GetFollowingUsersAsync(string cursor = null);
        Task<IEnumerable<UserBoard>> SearchBoards(string query, string cursor = null);
        Task<IEnumerable<UserPin>> SearchPins(string query, string cursor = null);
    }
}