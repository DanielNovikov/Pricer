namespace Pricer.Extensions;

public static class DateTimeExtensions
{
    public static string ToRelative(this DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;
        
        if (timeSpan.Days >= 365)
        {
            var years = timeSpan.Days / 365;
            return years switch
            {
                1 => "Рік тому",
                >= 2 and <= 4 => years + " роки тому",
                _ => years + " років тому"
            };
        }
        
        if (timeSpan.Days >= 30)
        {
            var months = timeSpan.Days / 30;
            return months switch
            {
                1 => "Місяць тому",
                >= 2 and <= 4 => months + " місяці тому",
                _ => months + " місяців тому"
            };
        }

        if (timeSpan.Days >= 7)
        {
            var weeks = timeSpan.Days / 7;

            return weeks switch
            {
                1 => "Тиждень тому",
                >= 2 and <= 4 => weeks + " тижні тому",
                _ => weeks + " тижнів тому"
            };
        }

        if (timeSpan.Days > 0)
        {
            var days = timeSpan.Days;
            return days switch
            {
                1 => "День тому",
                >= 2 and <= 4 => days + " дні тому",
                _ => days + " днів тому"
            };
        }

        if (timeSpan.Hours > 0)
        {
            var hours = timeSpan.Hours;
            return hours switch
            {
                1 => "Годину тому",
                >= 2 and <= 4 => hours + " години тому",
                >= 5 and <= 20 => hours + " годин тому",
                21 => hours + "годину тому",
                _ => hours + " години тому"
            };
        }

        if (timeSpan.Minutes > 0)
        {
            var minutes = timeSpan.Minutes;
            return minutes switch
            {
                1 => "Хвилину тому",
                >= 2 and <= 4 => minutes + " хвилини тому",
                >= 5 and <= 20 => minutes + " хвилин тому",
                21 => minutes + "хвилину тому",
                >= 22 and <= 24 => minutes + " хвилини тому",
                >= 25 and <= 30 => minutes + " хвилин тому",
                31 => minutes + "хвилину тому",
                >= 32 and <= 34 => minutes + " хвилини тому",
                >= 35 and <= 40 => minutes + " хвилин тому",
                41 => minutes + "хвилину тому",
                >= 42 and <= 44 => minutes + " хвилини тому",
                >= 45 and <= 50 => minutes + " хвилин тому",
                51 => minutes + "хвилину тому",
                >= 52 and <= 54 => minutes + " хвилини тому",
                _ => minutes + " хвилин тому"
            };
        }
        
        return "Тільки що";
    }
}