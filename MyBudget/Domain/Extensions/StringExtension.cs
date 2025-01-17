using System.Text.RegularExpressions;

namespace MyBudget.Domain.Extensions
{
    public static class StringExtension
    {
        public static string RemoveMultipleWhiteSpaces(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, @"\s+", " ").Trim().ToUpper();
        }

        public static string RemoveWhiteSpaces(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, @"\s+", "");
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, "[^0-9a-zA-Z]+", "");
        }
    }
}