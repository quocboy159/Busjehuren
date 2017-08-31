using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Common.Extensions
{
    public static class DateExtensions
    {
        public static string ToStringByCultureInfo(this DateTime date, string format = "dd MMMM yyyy", string culture = "nl-NL")
        {
            CultureInfo cultureInfo = new CultureInfo(culture);
            return date.ToString(format, cultureInfo);
        }

        public static DateTime DefaultValidDate(this DateTime date, List<string> holidays, List<int> availableDays, string format = "MM-dd")
        {
            TimeSpan maxDuration = TimeSpan.FromSeconds(10);
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            while (sw.Elapsed < maxDuration && true)
            {
                // is sunday and exist in holidays
                if (date.Day != 0 && !holidays.Contains(date.ToString(format)) && availableDays.Contains((int)date.DayOfWeek + 1))
                {
                    break;
                }
                else
                {
                    date = date.AddDays(1);
                }
            }

            sw.Stop();

            return date;
        }

        public static string GetDayNameByCultureInfo(this int dayOfWeek, string culture = "nl-NL")
        {
            return System.Globalization.CultureInfo.CreateSpecificCulture(culture).DateTimeFormat.GetDayName((DayOfWeek)dayOfWeek);
        }

        public static string GetDayNameByCultureInfo(this string dayOfWeek, string culture = "nl-NL")
        {
            if (string.IsNullOrEmpty(dayOfWeek))
                return dayOfWeek;

            return System.Globalization.CultureInfo.CreateSpecificCulture(culture).DateTimeFormat.GetDayName(EnumExtensions.ParseEnum<DayOfWeek>(dayOfWeek));
        }

        public static string GetAbbreviatedDayNameByCultureInfo(this string dayOfWeek, string culture = "nl-NL")
        {
            if (string.IsNullOrEmpty(dayOfWeek))
                return dayOfWeek;

            return System.Globalization.CultureInfo.CreateSpecificCulture(culture).DateTimeFormat.GetAbbreviatedDayName(EnumExtensions.ParseEnum<DayOfWeek>(dayOfWeek));
        }

    }


}
