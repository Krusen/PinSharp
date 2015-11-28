using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp.Api
{
    public partial class PinterestApi : IUsersApi
    {
        public Task<dynamic> GetUserAsync(string userName, IEnumerable<string> fields)
        {
            return GetAsync<dynamic>($"users/{userName}", fields);
        }

        public Task<UserDetails> GetUserAsync(string userName)
        {
            return GetAsync<UserDetails>($"users/{userName}", UserFields);
        }
    }
}
