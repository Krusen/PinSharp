using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public interface IPinsApi
    {
        Task<dynamic> GetPinAsync(string id, IEnumerable<string> fields);
        Task<Pin> GetPinAsync(string id);

        Task CreatePin(string board, string note, string link, string imageUrl);
        Task CreatePinFromBase64(string board, string note, string link, string imageBase64);
        Task DeletePin(string id);
        Task EditPin(string id, string board, string note, string link);
    }
}