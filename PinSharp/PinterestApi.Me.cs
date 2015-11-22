using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinSharp.Models;

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

        // TODO: Implement limit and paging
        public async Task<IEnumerable<UserPin>> GetPinsAsync(int limit = 0, string cursor = null)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            return await Get<IEnumerable<UserPin>>("me/pins", fields);
        }

        // TODO: Implement limit and paging
        public async Task<IEnumerable<UserPin>> GetLikedPinsAsync(int limit = 0, string cursor = null)
        {
            var fields = PinFields.Where(x => !x.StartsWith("creator"));
            return await Get<IEnumerable<UserPin>>("me/likes", fields);
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(string cursor = null)
        {
            return await Get<IEnumerable<User>>("me/followers", UserFields);
        }

        public async Task<IEnumerable<Board>> GetSuggestedBoardsAsync(string cursor = null)
        {
            return await Get<IEnumerable<Board>>("me/boards/suggested", BoardFields);
        }

        public async Task<IEnumerable<Board>> GetFollowingBoardsAsync(string cursor = null)
        {
            return await Get<IEnumerable<Board>>("me/following/boards", BoardFields);
        }

        public async Task<IEnumerable<Interest>> GetFollowingInterestsAsync(string cursor = null)
        {
            return await Get<IEnumerable<Interest>>("me/following/interests");
        }

        public async Task<IEnumerable<User>> GetFollowingUsersAsync(string cursor = null)
        {
            return await Get<IEnumerable<User>>("me/following/users");
        }

        public async Task<IEnumerable<UserBoard>> SearchBoardsAsync(string query, string cursor = null)
        {
            var fields = BoardFields.Where(x => !x.StartsWith("creator"));
            return await Get<IEnumerable<UserBoard>>($"me/search/boards/?query={query}", fields);
        }

        public async Task<IEnumerable<UserPin>> SearchPinsAsync(string query, string cursor = null)
        {
            var fields = UserFields.Where(x => !x.StartsWith("creator"));
            return await Get<IEnumerable<UserPin>>($"me/search/pins?query={query}", fields);
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
