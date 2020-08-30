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

        public void AddEvent(   string summary,
                                string location,
                                string description,
                                DateTime eventStartDateTime,
                                DateTime eventEndDateTime,
                                string timezone,
                                bool isRecurringEvent=false,
                                string recurringFrequency="",
                                int nbOfRecurringEvents=0)
        {
            Event newEvent = new Event()
            {
                Summary = summary,
                Location = location,
                Description = description
            };

            DateTime startDateTime = eventStartDateTime;
            EventDateTime start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = timezone
            };
            newEvent.Start = start;

            DateTime endDateTime = eventEndDateTime;
            EventDateTime end = new EventDateTime()
            {
                DateTime = endDateTime,
                TimeZone = timezone
            };
            newEvent.End = end;

            if (isRecurringEvent)
            {
                String[] recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=7" };
                newEvent.Recurrence = recurrence.ToList();
            }

            // FOR FUTURE - ADD ATTENDEES
            //EventAttendee[] attendees = new EventAttendee[]
            //{
            //    new EventAttendee() { Email = "<---email-address--->@gmail.com" },
            //};
            //newEvent.Attendees = attendees.ToList();

            String calendarId = "primary";

            var createdEvent = calendarService.Events.Insert(newEvent, calendarId).Execute();
            Console.WriteLine($"Event created {createdEvent.HtmlLink}");
        }

        /// <summary>
        /// Delete Google Calendar event by Id:
        /// assuming user using the code will click in the web app on Event
        /// and will retrieve eventId from that
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public void DeleteEvent(string eventId)
        {
            String calendarId = "primary";

            var createdEvent = calendarService.Events.Delete(calendarId, eventId).Execute();
            Console.WriteLine($"Event deleted {eventId}");
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

        public void UpdateEvent(string eventId, Event eventToUpdate)
        {
            String calendarId = "primary";

            var createdEvent = calendarService.Events.Update(eventToUpdate, calendarId, eventId).Execute();
            Console.WriteLine($"Event updated {eventId}");
        }

        // --------------------------------------------------------
        public IList<CalendarListEntry> GetAllUserCalendars()
        {
            var userCalendars = calendarService.CalendarList.List().Execute();

            // DEBUG
            //Console.WriteLine("User's available calendars are:");
            //foreach (var calendar in userCalendars.Items)
            //{
            //    Console.WriteLine($"{calendar.Id}");
            //    Console.WriteLine($"{calendar.Summary}");
            //    Console.WriteLine($"Access role: {calendar.AccessRole}");
            //}

            return userCalendars.Items;
        }
    }
}
