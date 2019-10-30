﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PinSharp.Api;
using PinSharp.Extensions;

namespace PinSharp
{
    // TODO: Add Oauth exception classes and handling
    /// <summary>
    /// Static class used for getting an authorization URL and to get an access token from the code returned from Pinterest.
    /// </summary>
    public static class PinSharpAuthClient
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
        ///     Call <see cref="BuildAuthorizationUrl(string, string, Scopes, string)"/> if you want to specify the state value yourself to be able to prevent spoofing.
        /// </para>
        /// <para>
        ///     "code" is used with <see cref="GetAccessTokenAsync"/> to
        ///     get an access token to use with <see cref="PinSharpClient"/>.
        /// </para>
        /// </summary>
        /// <param name="clientId">The Client ID (also known as App ID) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="redirectUri">
        ///     The URL you want your user to be redirected to after authorizing your app.
        ///     The code needed for <see cref="GetAccessTokenAsync"/> will be added as query string parameter "code".
        /// </param>
        /// <param name="scopes">The scopes you want to request from the user.</param>
        /// <returns></returns>
        public static string BuildAuthorizationUrl(string clientId, string redirectUri, Scopes scopes)
        {
            return BuildAuthorizationUrl(clientId, redirectUri, scopes, CreateRandomState());
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
        ///     get an access token to use with <see cref="PinSharpClient"/>.
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
        public static string BuildAuthorizationUrl(string clientId, string redirectUri, Scopes scopes, string state)
        {
            var scope = GetScope(scopes);

            return $"{BaseUrl}oauth/?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&state={state}";
        }

        /// <summary>
        /// Gets an access token which you can then use with <see cref="PinSharpClient"/>.
        /// </summary>
        /// <param name="clientId">The Client ID (also known as App ID) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="clientSecret">The Client secret (also known as App secret) of your app. See https://developers.pinterest.com/apps/</param>
        /// <param name="code">The code that was passed to your <c>redirectUri</c> as a query string parameter.</param>
        /// <param name="apiVersion">The API version. Defaults to "v1" if left out.</param>
        /// <returns>An access token for use with <see cref="PinSharpClient"/>.</returns>
        public static async Task<string> GetAccessTokenAsync(string clientId, string clientSecret, string code, string apiVersion = "v1")
        {
            var url = $"{BaseUrl}{apiVersion}/oauth/token?grant_type=authorization_code&client_id={clientId}&client_secret={clientSecret}&code={code}";

            var client = new HttpClient();
            var response = await client.PostAsync(url, null).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            return json["access_token"].Value<string>();
        }

        /// <summary>
        /// Generates a random string that you can use to verify that
        /// the redirect back to your site or app wasn't spoofed.
        ///
        /// <para>
        ///     Pass this to <see cref="BuildAuthorizationUrl"/> to get the correct login URL.
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

        private static string GetScope(Scopes scopes)
        {
            var values = new List<string>();

            if (scopes.HasFlag(Scopes.ReadPublic))
                values.Add("read_public");

            if (scopes.HasFlag(Scopes.WritePublic))
                values.Add("write_public");

            if (scopes.HasFlag(Scopes.ReadRelationships))
                values.Add("read_relationships");

            if (scopes.HasFlag(Scopes.WriteRelationships))
                values.Add("write_relationships");

            return string.Join(",", values);
        }
    }
}
