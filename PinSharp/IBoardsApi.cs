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

        Task<PagedResponse<Pin>> GetPinsAsync(string board, string cursor = null, int limit = 0);
        Task<PagedResponse<T>> GetPinsAsync<T>(string board, IEnumerable<string> fields, string cursor = null, int limit = 0);

        Task<BoardDetails> CreateBoardAsync(string name, string description = "");
        Task<BoardDetails> UpdateBoardAsync(string board, string name = "", string description = "");
        Task DeleteBoardAsync(string board);
    }
}