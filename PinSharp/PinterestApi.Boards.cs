using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IBoardsApi
    {
        public async Task<dynamic> GetBoardAsync(string board, IEnumerable<string> fields)
        {
            return await Get<dynamic>($"boards/{board}", fields);
        }

        public async Task<dynamic> GetBoardAsync(string userName, string boardName, IEnumerable<string> fields)
        {
            return await GetBoardAsync($"{userName}/{boardName}", fields);
        }

        public async Task<BoardDetails> GetBoardAsync(string board)
        {
            return await Get<BoardDetails>($"boards/{board}", BoardFields);
        }

        public async Task<BoardDetails> GetBoardAsync(string userName, string boardName)
        {
            return await GetBoardAsync($"{userName}/{boardName}");
        }

        public async Task<IEnumerable<dynamic>> GetPinsAsync(string board, IEnumerable<string> fields, int limit = 0, string cursor = null)
        {
            return await Get<dynamic>($"boards/{board}/pins", fields);
        }

        public async Task<IEnumerable<dynamic>> GetPinsAsync(string userName, string boardName, IEnumerable<string> fields, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync($"{userName}/{boardName}", fields, limit, cursor);
        }

        // TODO: Do something about imageSizes
        public async Task<IEnumerable<Pin>> GetPinsAsync(string board, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null)
        {
            return await Get<IEnumerable<Pin>>($"boards/{board}/pins", PinFields);
        }

        public async Task<IEnumerable<Pin>> GetPinsAsync(string userName, string boardName, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync($"{userName}/{boardName}", imageSizes, limit, cursor);
        }

        public async Task<BoardDetails> CreateBoardAsync(string name, string description = "")
        {
            return await Post<BoardDetails>("boards", new {name, description});
        }

        public async Task<BoardDetails> UpdateBoardAsync(string board, string name = "", string description = "")
        {
            return await Patch<BoardDetails>($"boards/{board}", new {board, name, description});
        }

        public async Task DeleteBoardAsync(string board)
        {
            await Delete($"boards/{board}");
        }
    }
}
