using System;
using System.Threading.Tasks;

namespace PinSharp.Models
{
    public class PinterestPinApi : PinterestApiBase
    {
        public PinterestPinApi(string accessToken, string apiVersion)
            : base(accessToken, apiVersion)
        {
        }

        public async Task<Pin> GetPinAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
