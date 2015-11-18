using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public interface IPinsApi
    {
        Task<Pin> GetPinAsync(string id);
    }
}