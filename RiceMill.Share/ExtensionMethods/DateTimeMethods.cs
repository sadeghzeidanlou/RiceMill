using MD.PersianDateTime.Standard;

namespace Shared.ExtensionMethods
{
    public static class DateTimeMethods
    {
        /// <summary>
        /// Get a number like 20200316 and return a DateTime object
        /// </summary>
        /// <param name="date">Number that represent DateTime</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int date) => new (date / 10000, date / 100 % 100, date % 100);

        /// <summary>
        /// Get a string like (2020-03-16) or (2020/03/16) then extract numbers and return a DateTime object
        /// </summary>
        /// <param name="date">string that represent DateTime</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string date) => date.ExtractInt32().ToDateTime();

        /// <summary>
        /// Get a number like 14010316 and return a PersianDateTime object
        /// </summary>
        /// <param name="date">Number that represent PersianDate</param>
        /// <returns></returns>
        public static PersianDateTime ToPersianDateTime(this int date) => new(date / 10000, date / 100 % 100, date % 100);

        /// <summary>
        /// Get a string like (1401-03-16) or (1401/03/16) then extract numbers and return a PersianDateTime object
        /// </summary>
        /// <param name="date">string that represent PersianDateTime </param>
        /// <returns></returns>
        public static PersianDateTime ToPersianDateTime(this string date) => date.ExtractInt32().ToPersianDateTime();

        /// <summary>
        /// Get total day difference between two DateTime object
        /// </summary>
        /// <param name="date1">First DateTime</param>
        /// <param name="date2">Second DateTime</param>
        /// <returns></returns>
        public static int TotalDayDifference(this DateTime date1, DateTime date2) => (int)(date1 - date2).TotalDays;

        /// <summary>
        /// Get total day difference between two PersianDateTime object
        /// </summary>
        /// <param name="date1">First PersianDateTime</param>
        /// <param name="date2">Second PersianDateTime</param>
        /// <returns></returns>
        public static int TotalDayDifference(this PersianDateTime date1, PersianDateTime date2) => (int)(date1 - date2).TotalDays;
    }
}