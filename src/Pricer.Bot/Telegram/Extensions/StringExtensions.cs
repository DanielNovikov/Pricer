namespace Pricer.Bot.Telegram.Extensions;

public static class StringExtensions
{
    public static long ToLong(this string value)
    {
        return long.Parse(value);
    }
}