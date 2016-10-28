using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Api.Responses;
using PinSharp.Models;

namespace PinSharp.Api
{
    public interface IBoardsApi
    {
        Task<IDetailedBoard> GetBoardAsync(string board);
        Task<T> GetBoardAsync<T>(string board, IEnumerable<string> fields);

        Task<PagedResponse<IPin>> GetPinsAsync(string board);
        Task<PagedResponse<IPin>> GetPinsAsync(string board, int limit);
        Task<PagedResponse<IPin>> GetPinsAsync(string board, string cursor);
        Task<PagedResponse<IPin>> GetPinsAsync(string board, string cursor, int limit);

        Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields);
        Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit);
        Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor);
        Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor, int limit);

        Task<IDetailedBoard> CreateBoardAsync(string name, string description = null);
        Task<IDetailedBoard> UpdateBoardAsync(string board, string name = null, string description = null);
        Task DeleteBoardAsync(string board);
    }
}