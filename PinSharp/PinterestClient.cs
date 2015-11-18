using System.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public class PinterestClient
    {
        // TODO: Set should also set on attached APIs
        public string AccessToken { get; set; }
        public string ApiVersion { get; set; }

        private PinterestApi Api { get; set; }

        public IBoardsApi Boards => Api;
        public IMeApi Me => Api;
        public IUsersApi Users => Api;

        public PinterestClient(string accessToken, string apiVersion = "v1")
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;

            Api = new PinterestApi(accessToken, apiVersion);
        }
    }
}
