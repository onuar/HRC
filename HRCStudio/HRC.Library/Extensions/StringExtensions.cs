namespace HRC.Library.Extensions
{
    public static class StringExtensions
    {
        public static string FormatText(this string value, params object[] parameters)
        {
            return string.Format(value, parameters);
        }
    }
}
