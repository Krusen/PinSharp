using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public interface IBoardsApi
    {
        Task<BoardDetails> GetBoardAsync(string userName, string boardName);
        Task<IEnumerable<Pin>> GetPinsAsync(string userName, string boardName, IEnumerable<int> imageSizes = null,  int limit = 0, string cursor = null);
        Task<BoardDetails> GetBoardAsync(string boardIdOrPath);
        Task<IEnumerable<Pin>> GetPinsAsync(string boardIdOrPath, IEnumerable<int> imageSizes = null, int limit = 0, string cursor = null);
    }
}