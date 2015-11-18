using System;
using System.Threading.Tasks;
using PinSharp.Models;

namespace PinSharp
{
    public partial class PinterestApi : IPinsApi
    {
        public async Task<Pin> GetPinAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
