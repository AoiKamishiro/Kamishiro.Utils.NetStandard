using System;
using System.Collections.Generic;

namespace Kamishiro.Utils.DotNetCore.Google
{
    public class CalendarEventEqualyCompare : IEqualityComparer<CalendarEvent>
    {
        public bool Equals(CalendarEvent c1, CalendarEvent c2)
        {
            if (DateTime.Compare(c1.StartTime, c2.StartTime) != 0)
            {
                return false;
            }
            if (DateTime.Compare(c1.EndTime, c2.EndTime) != 0)
            {
                return false;
            }
            if (string.Compare(c1.Summary, c2.Summary) != 0)
            {
                return false;
            }
            return true;
        }
        public int GetHashCode(CalendarEvent c)
        {
            int hCode = c.StartTime.GetHashCode() ^ c.EndTime.GetHashCode() ^ c.Summary.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
