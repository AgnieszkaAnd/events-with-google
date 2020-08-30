using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Calendar.v3.Data;

namespace GoogleCalendarTestApp
{
    public interface IGoogleCalendar
    {
        IEnumerable<Event> GetEvents();

        Event GetEventByDateTime(DateTime start, DateTime end);

        void AddEvent(  string summary,
                        string location,
                        string description,
                        DateTime eventStartDateTime,
                        DateTime eventEndDateTime,
                        string timezone,
                        bool isRecurringEvent = false,
                        string recurringFrequency = "",
                        int nbOfRecurringEvents = 0);

        void UpdateEvent(string eventId, Event eventToUpdate);

        void DeleteEvent(string eventId);
    }
}
