using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;
using PinSharp.Models.Responses;

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

        public async Task<PinResponse<Pin>> GetPinsAsync(string board)
        {
            return await GetPinsAsync<Pin>(board, PinFields, null, 0);
        }

        public async Task<PinResponse<Pin>> GetPinsAsync(string board, int limit)
        {
            return await GetPinsAsync<Pin>(board, PinFields, null, limit);
        }

        public async Task<PinResponse<Pin>> GetPinsAsync(string board, string cursor)
        {
            return await GetPinsAsync<Pin>(board, PinFields, cursor, 0);
        }

        public async Task<PinResponse<Pin>> GetPinsAsync(string board, string cursor, int limit)
        {
            return await GetPinsAsync<Pin>(board, PinFields, cursor, limit);
        }

        public async Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields)
        {
            return await GetPinsAsync<T>(board, fields, null, 0);
        }

        public async Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit)
        {
            return await GetPinsAsync<T>(board, fields, null, limit);
        }

        public async Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor)
        {
            return await GetPinsAsync<T>(board, fields, cursor, 0);
        }

        public async Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor, int limit)
        {
            var response = await GetPaged<T>($"boards/{board}/pins", fields, cursor, limit);
            return new PinResponse<T>(response.Data, response.Page?.Cursor);
        }

        public async Task<BoardDetails> CreateBoardAsync(string name, string description = "")
        {
            return await Post<BoardDetails>("boards", new {name, description}, BoardFields);
        }

        public async Task<BoardDetails> UpdateBoardAsync(string board, string name = "", string description = "")
        {
            return await Patch<BoardDetails>($"boards/{board}", new {board, name, description}, BoardFields);
        }

        public async Task DeleteBoardAsync(string board)
        {
            await Delete($"boards/{board}");
        }
    }
}
