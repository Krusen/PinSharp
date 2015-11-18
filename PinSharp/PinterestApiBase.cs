namespace PinSharp
{
    public abstract class PinterestApiBase
    {
        internal string AccessToken { get; set; }
        internal string ApiVersion { get; set; }

        protected PinterestApiBase(string accessToken, string apiVersion)
        {
            AccessToken = accessToken;
            ApiVersion = apiVersion;
        }

        protected string GetUrlWithFields(string url, string[] fields)
        {
            var fieldsString = string.Join(",", fields);
            return $"{url}?access_token={AccessToken}&fields={fieldsString}";
        }
    }
}
