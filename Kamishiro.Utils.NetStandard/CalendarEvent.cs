using System;

namespace Kamishiro.Utils.NetStandard.Google
{
    public class CalendarEvent
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Summary { get; set; }

        public CalendarEvent(DateTime startTime, DateTime endTime, string summery)
        {
            StartTime = startTime;
            EndTime = endTime;
            Summary = summery;
        }

        public string ToCalendarString()
        {
            return StartTime.Hour.ToString().PadLeft(2, '0') + ":"
                + StartTime.Minute.ToString().PadLeft(2, '0') + "～"
                + EndTime.Hour.ToString().PadLeft(2, '0') + ":"
                + EndTime.Minute.ToString().PadLeft(2, '0') + "  "
                + Summary;
        }
        public string ToDateString()
        {
            return StartTime.Month.ToString().PadLeft(2, '0') + "月" + StartTime.Day.ToString().PadLeft(2, '0') + "日 (" + Week(StartTime) + ")";
        }
        public string GetDateInfo()
        {
            return StartTime.Year.ToString().PadLeft(4, '0') + StartTime.Month.ToString().PadLeft(2, '0') + StartTime.Day.ToString().PadLeft(2, '0');
        }
        private static string Week(DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "日";
                case DayOfWeek.Monday:
                    return "月";
                case DayOfWeek.Tuesday:
                    return "火";
                case DayOfWeek.Wednesday:
                    return "水";
                case DayOfWeek.Thursday:
                    return "木";
                case DayOfWeek.Friday:
                    return "金";
                case DayOfWeek.Saturday:
                    return "土";
                default:
                    return "不";
            };
        }
    }
}
