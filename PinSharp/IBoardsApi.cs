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

        Task<PinResponse<Pin>> GetPinsAsync(string board, int limit = 0, string cursor = null);
        Task<PinResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, int limit = 0, string cursor = null);

        Task<BoardDetails> CreateBoardAsync(string name, string description = "");
        Task<BoardDetails> UpdateBoardAsync(string board, string name = "", string description = "");
        Task DeleteBoardAsync(string board);
    }
}