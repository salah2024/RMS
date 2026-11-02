using System.Globalization;

namespace RMS.Models.Common;

public static class DateTimeExtensions
{
    public static string ToPersianDateTime(this DateTime date)
    {
        var pc = new PersianCalendar();

        return $"{pc.GetYear(date):0000}/" +
               $"{pc.GetMonth(date):00}/" +
               $"{pc.GetDayOfMonth(date):00} " +
               $"{date:HH:mm}";
    }
}
