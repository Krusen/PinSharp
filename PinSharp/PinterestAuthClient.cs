using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PinSharp
{
    public static class PinterestAuthClient
    {
        private const string BaseUrl = "https://api.pinterest.com/";

        /// <summary>
        /// <para>
        ///     Generates a login URL with the required parameters.
        ///     Users will need to visit this URL to authorize your app to use the API on their behalf.
        /// </para>
        /// <para>
        ///     If they accept they will be redirected to the <paramref name="redirectUri"/>
        ///     with two query string parameters - "state" and "code".
        /// </para>
        /// <para>
        ///     Call <see cref="GetLoginUrl(string,string,PinterestScopes,string)"/> if you want to specify the state value yourself to be able to prevent spoofing.
        /// </para>
        /// <para>
        ///     "code" is used with <see cref="GetAccessTokenAsync"/> to
        ///     get an access token to use with <see cref="PinterestClient"/>.
        /// </para>
        /// </summary>
        /// <param name="clientId">The Client ID (also known as App ID) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="redirectUri">
        ///     The URL you want your user to be redirected to after authorizing your app.
        ///     The code needed for <see cref="GetAccessTokenAsync"/> will be added as query string parameter "code".
        /// </param>
        /// <param name="scopes">The scopes you want to request from the user.</param>
        /// <returns></returns>
        public static string GetLoginUrl(string clientId, string redirectUri, PinterestScopes scopes)
        {
            return GetLoginUrl(clientId, redirectUri, scopes, CreateRandomState());
        }

        /// <summary>
        /// <para>
        ///     Generates a login URL with the required parameters.
        ///     Users will need to visit this URL to authorize your app to use the API on their behalf.
        /// </para>
        /// <para>
        ///     If they accept they will be redirected to the <paramref name="redirectUri"/>
        ///     with two query string parameters - "state" and "code".
        /// </para>
        /// <para>
        ///     "state" verifies that this comes from you.
        ///     "code" is used with <see cref="GetAccessTokenAsync"/> to
        ///     get an access token to use with <see cref="PinterestClient"/>.
        /// </para>
        /// </summary>
        /// <param name="clientId">The Client ID (also known as App ID) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="redirectUri">
        ///     The URL you want your user to be redirected to after authorizing your app.
        ///     The code needed for <see cref="GetAccessTokenAsync"/> will be added as query string parameter "code".
        /// </param>
        /// <param name="scopes">The scopes you want to request from the user.</param>
        /// <param name="state">A string that is added to <paramref name="redirectUri"/> as query string parameter "state". This is to prevent spoofing.</param>
        /// <returns></returns>
        public static string GetLoginUrl(string clientId, string redirectUri, PinterestScopes scopes, string state)
        {
            var scope = GetScope(scopes);

            return $"{BaseUrl}oauth/?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&state={state}";
        }

        /// <summary>
        /// Gets an access token which you can then use with <see cref="PinterestClient"/>.
        /// </summary>
        /// <param name="clientId">The Client ID (also known as App ID) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="clientSecret">The Client secret (also known as App secret) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="code">The code that was passed to your <c>redirectUri</c> as a query string parameter.</param>
        /// <param name="apiVersion">The API version. Defaults to "v1" if left out.</param>
        /// <returns>An access token for use with <see cref="PinterestClient"/>.</returns>
        public static async Task<string> GetAccessTokenAsync(string clientId, string clientSecret, string code, string apiVersion = "v1")
        {
            var url = $"{BaseUrl}{apiVersion}/oauth/token?grant_type=authorization_code&client_id={clientId}&client_secret={clientSecret}&code={code}";

            var client = new System.Net.Http.HttpClient();
            var response = await client.PostAsync(url, null);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsAsync<dynamic>();
            return json.access_token;
        }

        /// <summary>
        /// Generates a random string that you can use to verify that
        /// the redirect back to your site or app wasn't spoofed.
        ///
        /// <para>
        ///     Pass this to <see cref="GetLoginUrl(string,string,PinterestScopes,string)"/> to get the correct login URL.
        /// </para>
        /// </summary>
        /// <param name="length">The length of the random string.</param>
        /// <returns></returns>
        public static string CreateRandomState(int length = 10)
        {
            var data = new byte[length/2];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }

        private static string GetScope(PinterestScopes permissions)
        {
            var scopes = new List<string>();

            if (permissions.HasFlag(PinterestScopes.ReadPublic))
                scopes.Add("read_public");

            if (permissions.HasFlag(PinterestScopes.WritePublic))
                scopes.Add("write_public");

            if (permissions.HasFlag(PinterestScopes.ReadRelationships))
                scopes.Add("read_relationships");

            if (permissions.HasFlag(PinterestScopes.WriteRelationShips))
                scopes.Add("write_relationships");

            return string.Join(",", scopes);
        }
    }
}
