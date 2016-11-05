using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    internal partial class PinterestApi : IBoardsApi
    {
        public Task<IDetailedBoard> GetBoardAsync(string board)
        {
            return GetBoardAsync<IDetailedBoard>(board, BoardFields);
        }

        public Task<T> GetBoardAsync<T>(string board, IEnumerable<string> fields)
        {
            return GetAsync<T>($"boards/{board}", new RequestOptions(fields));
        }

        public Task<PagedResponse<IPin>> GetPinsAsync(string board)
        {
            return GetPinsAsync<IPin>(board, PinFields, null, 0);
        }

        public Task<PagedResponse<IPin>> GetPinsAsync(string board, int limit)
        {
            return GetPinsAsync<IPin>(board, PinFields, null, limit);
        }

        public Task<PagedResponse<IPin>> GetPinsAsync(string board, string cursor)
        {
            return GetPinsAsync<IPin>(board, PinFields, cursor, 0);
        }

        public Task<PagedResponse<IPin>> GetPinsAsync(string board, string cursor, int limit)
        {
            return GetPinsAsync<IPin>(board, PinFields, cursor, limit);
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
            var response = await GetPagedAsync<T>($"boards/{board}/pins", new RequestOptions(fields, cursor, limit)).ConfigureAwait(false);
            return new PagedResponse<T>(response.Data, response.Page?.Cursor);
        }

        public Task<IDetailedBoard> CreateBoardAsync(string name, string description = null)
        {
            return PostAsync<IDetailedBoard>("boards", new {name, description}, new RequestOptions(BoardFields));
        }

        public Task<IDetailedBoard> UpdateBoardAsync(string board, string name, string description = null)
        {
            return PatchAsync<IDetailedBoard>($"boards/{board}", new {board, name, description}, new RequestOptions(BoardFields));
        }

        public Task DeleteBoardAsync(string board)
        {
            return DeleteAsync($"boards/{board}");
        }
    }
}
