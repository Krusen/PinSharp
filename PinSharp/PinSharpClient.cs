using PinSharp.Api;
using PinSharp.Http;

namespace PinSharp
{
    public class PinSharpClient
    {
        private PinterestApi Api { get; }

        /// <summary>
        ///  Endpoints for getting board information, pins on boards and managing boards.
        /// </summary>
        public IBoardsApi Boards => Api;

        /// <summary>
        /// Endspoints related to the user associated with the used access token.
        /// </summary>
        public IMeApi Me => Api;

        /// <summary>
        /// Endspoints for creating, updating and deleting pins.
        /// </summary>
        public IPinsApi Pins => Api;

        /// <summary>
        /// Endpoints for getting user information.
        /// </summary>
        public IUsersApi Users => Api;

        /// <summary>
        /// Contains information about your request limit and remaining requests.
        /// Rate limits uses a 60-minute sliding window.
        /// This is updated after each request and will be null until the first requests has been made.
        /// </summary>
        public IRateLimits RateLimits => Api.RateLimits;

        public PinSharpClient(string accessToken, string apiVersion)
        {
            Api = new PinterestApi(accessToken, apiVersion);
        }

        public PinSharpClient(IHttpClient httpClient)
        {
            Api = new PinterestApi(httpClient);
        }
    }
}
