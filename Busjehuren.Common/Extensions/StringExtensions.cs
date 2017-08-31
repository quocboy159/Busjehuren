using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Busjehuren.Common.Extensions
{
    public static class StringExtensions
    {
        public static List<int> AllIndexesOf(this string text, string value)
        {
            int index = 0;
            var result = new List<int>();

            while (text.IndexOf(value, index + 1) != -1)
            {
                index = text.IndexOf(value, index + 1);
                result.Add(index);
            }

            return result;
        }

        public static string LimitLength(this string text, int limit = 0)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Length > limit ? text.Substring(0, limit) : text;
        }

        public static string RemoveIllegalCharacters(this string text)
        {
            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", "-");
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", "-");
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace("+", "-");
            text = text.Replace("“", string.Empty);
            text = text.Replace("”", string.Empty);
            text = text.Replace("(", string.Empty);
            text = text.Replace(")", string.Empty);
            text = text.Replace("!", string.Empty);
            text = text.Replace("đ", "d");
            text = text.Replace("Đ", "d");
            text = text.Replace("‘", string.Empty);
            text = text.Replace("’", string.Empty);
            text = text.Replace(" ", "-");
            text = text.Replace("$", string.Empty);
            text = RemoveExtraHyphen(text);
            text = text.Trim('-');

            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }

        private static string RemoveExtraHyphen(this string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }

        public static string FormatByCulture(this decimal number, string culture = "nl-NL")
        {
            return number.ToString("C", new CultureInfo(culture));
        }

    }
}
