using System.Text.RegularExpressions;

namespace Zip.WebAPI.Extentions
{
    public static class StringExtensions
    {
        private const string EMAIL_PATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public static bool IsValidEmailAddress(this string s)
        {
            return Regex.IsMatch(s, EMAIL_PATTERN);
        }
    }
}
