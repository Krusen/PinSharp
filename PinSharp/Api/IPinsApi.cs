using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp.Api
{
    public interface IPinsApi
    {
        Task<dynamic> GetPinAsync(string id, IEnumerable<string> fields);
        Task<IPin> GetPinAsync(string id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="board">The board ID or slug (user/board-name)</param>
        /// <param name="imageUrl"></param>
        /// <param name="note"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        Task<IPin> CreatePinAsync(string board, string imageUrl, string note, string link = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="board">The board ID or slug (user/board-name)</param>
        /// <param name="imageBase64"></param>
        /// <param name="note"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        Task<IPin> CreatePinFromBase64Async(string board, string imageBase64, string note, string link = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="board">The new board ID or slug (user/board-name)</param>
        /// <param name="note"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        Task<IPin> UpdatePinAsync(string id, string board, string note, string link);

        Task DeletePinAsync(string id);
    }
}