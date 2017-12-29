using System;

public static class DateTimeExtensions
{
    public static string ToSQLDateString(this DateTime? dateTime)
    {
        return dateTime.GetValueOrDefault().ToSQLDateString();
    }

    public static string ToSQLDateString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd");
    }

    public static string ToSQLDateTimeString(this DateTime? dateTime)
    {
        return dateTime.GetValueOrDefault().ToSQLDateTimeString();
    }

    public static string ToSQLDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}