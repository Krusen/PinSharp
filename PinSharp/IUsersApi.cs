using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public interface IUsersApi
    {
        Task<UserDetails> GetUserAsync(string userName);
    }
}