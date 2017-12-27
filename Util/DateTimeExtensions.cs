using System;

public static class DateTimeExtensions
{
    public static string ToSQLString(this DateTime? dateTime)
    {
        return dateTime.GetValueOrDefault().ToSQLString();
    }

    public static string ToSQLString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd");
    }
}