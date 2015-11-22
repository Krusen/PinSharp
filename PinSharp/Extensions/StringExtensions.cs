namespace PinSharp.Extensions
{
    public static class StringExtensions
    {
        public static string EnsurePostfix(this string str, string end)
        {
            if (str.EndsWith(end))
                return str;
            return str + end;
        }
    }
}
