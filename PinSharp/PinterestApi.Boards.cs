using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IBoardsApi
    {
        public async Task<BoardDetails> GetBoardAsync(string board)
        {
            return await GetBoardAsync<BoardDetails>(board, BoardFields);
        }

        public async Task<T> GetBoardAsync<T>(string board, IEnumerable<string> fields)
        {
            return await Get<T>($"boards/{board}", fields);
        }

        // TODO: Image sizes, limit and cursor
        public async Task<IEnumerable<Pin>> GetPinsAsync(string board, int limit = 0, string cursor = null)
        {
            return await GetPinsAsync<Pin>(board, PinFields, limit, cursor);
        }

        public async Task<IEnumerable<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit = 0, string cursor = null)
        {
            return await Get<IEnumerable<T>>($"boards/{board}/pins", fields);
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
