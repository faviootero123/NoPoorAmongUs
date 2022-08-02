namespace SaucyCapstone.Static;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt)
    {
        int diff = (7 + (int)dt.Date.DayOfWeek) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}