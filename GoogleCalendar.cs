using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace GoogleCalendarTestApp
{
    public class GoogleCalendar : IGoogleCalendar
    {
        private readonly CalendarService calendarService;

        public GoogleCalendar(  IGoogleAPIconnection googleAPIconnection,
                                string appName)
        {
            var token = googleAPIconnection.GetToken();
            calendarService = googleAPIconnection.CreateCalendarService(token, appName);
        }

        public bool AddEvent()
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent()
        {
            throw new NotImplementedException();
        }

        public Event GetEventByDateTime(DateTime start, DateTime end)
        {
            EventsResource.ListRequest request = calendarService.Events.List("primary");
            request.TimeMin = start;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1;
            Event eventResult = request.Execute().Items.FirstOrDefault();

            // DEBUG
            //Console.WriteLine($"{eventResult.Summary}\n {eventResult.Location}\n {eventResult.Start.DateTime}");

            return eventResult.End.DateTime <= end ? eventResult : null;
        }

        public IEnumerable<Event> GetEvents()
        {
            // Define parameters of request.
            EventsResource.ListRequest request = calendarService.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            } else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();

            return events.Items;
        }

        public bool UpdateEvent()
        {
            throw new NotImplementedException();
        }

        // --------------------------------------------------------
        public IList<CalendarListEntry> GetAllUserCalendars()
        {
            var userCalendars = calendarService.CalendarList.List().Execute();
            
            // DEBUG
            //Console.WriteLine("User's available calendars are:");
            //foreach (var calendar in userCalendars.Items) 
            //{
            //    Console.WriteLine($"{calendar.Summary}");
            //}

            return userCalendars.Items;
        }
    }
}
