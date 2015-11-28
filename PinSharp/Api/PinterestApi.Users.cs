using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp.Api
{
    public partial class PinterestApi : IUsersApi
    {
        public async Task<dynamic> GetUserAsync(string userName, IEnumerable<string> fields)
        {
            return await GetAsync<dynamic>($"users/{userName}", fields);
        }

        public async Task<UserDetails> GetUserAsync(string userName)
        {
            return await GetAsync<UserDetails>($"users/{userName}", UserFields);
        }
    }
}
