namespace common.Model
{
    public static class StringExtension
    {
        public static string GetLast(this string source, int length)
        {
            if (length >= source.Length)
                return source;
            return source.Substring(source.Length - length);
        }
    }
}
