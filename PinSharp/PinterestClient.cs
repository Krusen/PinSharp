namespace PinSharp
{
    public class PinterestClient
    {
        public string AccessToken { get; }
        public string ApiVersion { get; }

        private PinterestApi Api { get; }

        public IBoardsApi Boards => Api;
        public IMeApi Me => Api;
        public IPinsApi Pins => Api;
        public IUsersApi Users => Api;

        public PinterestClient(string accessToken, string apiVersion = "v1")
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;

            Api = new PinterestApi(accessToken, apiVersion);
        }
    }
}
