using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PinSharp
{
    internal class FormUrlEncodedContent : ByteArrayContent
    {
        private static readonly Encoding DefaultHttpEncoding = Encoding.GetEncoding("ISO-8859-1");

        public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
          : base(GetContentByteArray(nameValueCollection))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

        private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection == null) throw new ArgumentNullException(nameof(nameValueCollection));

            var stringBuilder = new StringBuilder();
            foreach (var keyValuePair in nameValueCollection)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append('&');
                stringBuilder.Append(Encode(keyValuePair.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(Encode(keyValuePair.Value));
            }
            return DefaultHttpEncoding.GetBytes(stringBuilder.ToString());
        }

        private static string Encode(string data)
        {
            return string.IsNullOrEmpty(data) ? "" : Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
}
