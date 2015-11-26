using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp.Api
{
    public interface IUsersApi
    {
        Task<dynamic> GetUserAsync(string userName, IEnumerable<string> fields);
        Task<UserDetails> GetUserAsync(string userName);
    }
}