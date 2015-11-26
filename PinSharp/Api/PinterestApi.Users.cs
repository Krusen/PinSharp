using System.Collections.Generic;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp.Api
{
    public partial class PinterestApi : IUsersApi
    {
        public async Task<dynamic> GetUserAsync(string userName, IEnumerable<string> fields)
        {
            return await Get<dynamic>($"users/{userName}", fields);
        }

        public async Task<UserDetails> GetUserAsync(string userName)
        {
            return await Get<UserDetails>($"users/{userName}", UserFields);
        }
    }
}
