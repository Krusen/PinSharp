using System.Linq;
using PinSharp.Models;

namespace PinSharp
{
    public class PinterestClient
    {
        // TODO: Set should also set on attached APIs
        public string AccessToken { get; set; }
        public string ApiVersion { get; set; }

        public PinterestBoardApi Boards { get; private set; }
        public PinterestMeApi Me { get; private set; }
        public PinterestUserApi Users { get; private set; }

        public PinterestClient(string accessToken, string apiVersion = "v1")
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;

            Boards = new PinterestBoardApi(accessToken, apiVersion);
            Me = new PinterestMeApi(accessToken, apiVersion);
            Users = new PinterestUserApi(accessToken, apiVersion);
        }
    }
}
