using PinSharp.Api;

namespace PinSharp
{
    public class PinterestClient
    {
        private PinterestApi Api { get; }

        public IBoardsApi Boards => Api;
        public IMeApi Me => Api;
        public IPinsApi Pins => Api;
        public IUsersApi Users => Api;

        public PinterestClient(string accessToken, string apiVersion = "v1")
        {
            Api = new PinterestApi(accessToken, apiVersion);
        }

        public PinterestClient(IHttpClient httpClient)
        {
            Api = new PinterestApi(httpClient);
        }
    }
}
