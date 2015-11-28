using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    public partial class PinterestApi : IBoardsApi
    {
        public Task<BoardDetails> GetBoardAsync(string board)
        {
            return GetBoardAsync<BoardDetails>(board, BoardFields);
        }

        public Task<T> GetBoardAsync<T>(string board, IEnumerable<string> fields)
        {
            return GetAsync<T>($"boards/{board}", fields);
        }

        public Task<PagedResponse<Pin>> GetPinsAsync(string board)
        {
            return GetPinsAsync<Pin>(board, PinFields, null, 0);
        }

        public Task<PagedResponse<Pin>> GetPinsAsync(string board, int limit)
        {
            return GetPinsAsync<Pin>(board, PinFields, null, limit);
        }

        public Task<PagedResponse<Pin>> GetPinsAsync(string board, string cursor)
        {
            return GetPinsAsync<Pin>(board, PinFields, cursor, 0);
        }

        public Task<PagedResponse<Pin>> GetPinsAsync(string board, string cursor, int limit)
        {
            return GetPinsAsync<Pin>(board, PinFields, cursor, limit);
        }

        public Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields)
        {
            return GetPinsAsync<T>(board, fields, null, 0);
        }

        public Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit)
        {
            return GetPinsAsync<T>(board, fields, null, limit);
        }

        public Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor)
        {
            return GetPinsAsync<T>(board, fields, cursor, 0);
        }

        public async Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor, int limit)
        {
            var response = await GetPagedAsync<T>($"boards/{board}/pins", fields, cursor, limit).Configured();
            return new PagedResponse<T>(response.Data, response.Page?.Cursor);
        }

        public Task<BoardDetails> CreateBoardAsync(string name, string description = null)
        {
            return PostAsync<BoardDetails>("boards", new {name, description}, BoardFields);
        }

        public Task<BoardDetails> UpdateBoardAsync(string board, string name = null, string description = null)
        {
            return PatchAsync<BoardDetails>($"boards/{board}", new {board, name, description}, BoardFields);
        }

        public Task DeleteBoardAsync(string board)
        {
            return DeleteAsync($"boards/{board}");
        }
    }
}
