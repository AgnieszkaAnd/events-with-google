using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Calendar.v3.Data;

namespace GoogleCalendarTestApp
{
    public interface IGoogleCalendar
    {
        IEnumerable<Event> GetEvents();

        Event GetEventByName();

        bool AddEvent();

        bool UpdateEvent();

        bool DeleteEvent();
    }
}
