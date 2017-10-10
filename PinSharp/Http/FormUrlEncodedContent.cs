using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PinSharp.Http
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
            if (string.IsNullOrEmpty(data))
                return "";

            return EscapeLongDataString(data);
        }

        private static string EscapeLongDataString(string data)
        {
            // Uri.EscapeDataString() does not support strings longer than this
            const int maxLength = 65519;

            var sb = new StringBuilder();
            var iterationsNeeded = data.Length / maxLength;

            for (var i = 0; i <= iterationsNeeded; i++)
            {
                sb.Append(i < iterationsNeeded
                    ? Uri.EscapeDataString(data.Substring(maxLength * i, maxLength))
                    : Uri.EscapeDataString(data.Substring(maxLength * i)));
            }

            return sb.ToString().Replace("%20", "+");
        }
    }
}
