using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;
using PinSharp.Models.Responses;

namespace PinSharp
{
    public interface IBoardsApi
    {
        Task<BoardDetails> GetBoardAsync(string board);
        Task<T> GetBoardAsync<T>(string board, IEnumerable<string> fields);

        Task<PinResponse<Pin>> GetPinsAsync(string board);
        Task<PinResponse<Pin>> GetPinsAsync(string board, int limit);
        Task<PinResponse<Pin>> GetPinsAsync(string board, string cursor);
        Task<PinResponse<Pin>> GetPinsAsync(string board, string cursor, int limit);

        Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields);
        Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit);
        Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor);
        Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor, int limit);

        Task<BoardDetails> CreateBoardAsync(string name, string description = "");
        Task<BoardDetails> UpdateBoardAsync(string board, string name = "", string description = "");
        Task DeleteBoardAsync(string board);
    }
}