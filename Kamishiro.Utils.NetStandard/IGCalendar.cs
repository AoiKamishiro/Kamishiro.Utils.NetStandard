using System;
using System.Collections.Generic;

namespace Kamishiro.Utils.DotNetCore.Google
{
    public interface IGCalendar
    {
        string AppName { get; }
        string ClientEmail { get; }
        string PrivateKey { get; }

        List<CalendarEvent> GetEvents(string calenderID, DateTime minDate);
    }
}
