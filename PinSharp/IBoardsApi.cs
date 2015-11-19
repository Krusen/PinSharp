using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public interface IBoardsApi
    {
        Task<dynamic> GetBoardAsync(string board, IEnumerable<string> fields);
        Task<dynamic> GetBoardAsync(string userName, string boardName, IEnumerable<string> fields);
        Task<BoardDetails> GetBoardAsync(string board);
        Task<BoardDetails> GetBoardAsync(string userName, string boardName);

        Task<IEnumerable<dynamic>> GetPinsAsync(string board, IEnumerable<string> fields, int limit = 0, string cursor = null);
        Task<IEnumerable<dynamic>> GetPinsAsync(string userName, string boardName, IEnumerable<string> fields, int limit = 0, string cursor = null);
        Task<IEnumerable<Pin>> GetPinsAsync(string board, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null);
        Task<IEnumerable<Pin>> GetPinsAsync(string userName, string boardName, IEnumerable<int> imageSizes = null,  int limit = 0, string cursor = null);
    }
}