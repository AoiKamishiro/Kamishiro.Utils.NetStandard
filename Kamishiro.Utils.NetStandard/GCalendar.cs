using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kamishiro.Utils.NetStandard.Google
{
    public class GCalendar : IGCalendar
    {
        public string AppName { get; }
        public string ClientEmail { get; }
        public string PrivateKey { get; }
        private readonly CalendarService CalendarService;

        private static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        public GCalendar(string appName, string clientEmail, string privateKey)
        {
            AppName = appName;
            ClientEmail = clientEmail;
            PrivateKey = privateKey;
            CalendarService = GetService();
        }
        private CalendarService GetService()
        {
            ServiceAccountCredential credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(ClientEmail)
            {
                Scopes = Scopes
            }.FromPrivateKey(PrivateKey));

            // Create Google Calendar API service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName,
            });

            return service;
        }

        public List<CalendarEvent> GetEvents(string calenderID, DateTime minDate)
        {
            List<CalendarEvent> calenderEvents = new List<CalendarEvent>();
            bool isNextExist = true;

            while (isNextExist)
            {
                Console.WriteLine("Get 50 events since {0}.", minDate.ToString("yyyy/MM/dd HH:mm:ss"));
                EventsResource.ListRequest request = new EventsResource.ListRequest(CalendarService, calenderID)
                {
                    MaxResults = 50,
                    ShowDeleted = false,
                    TimeMin = minDate,
                    OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime,
                    SingleEvents = true
                };

                Events events = request.Execute();
                Console.WriteLine("There are {0} events retrieved.", events.Items.Count);
                if (events.Items != null && events.Items.Count > 1)
                {
                    foreach (Event eventItem in events.Items)
                    {
                        CalendarEvent calenderEvent = new CalendarEvent((DateTime)eventItem.Start.DateTime, (DateTime)eventItem.End.DateTime, eventItem.Summary);
                        calenderEvents = calenderEvents.Concat(new CalendarEvent[] { calenderEvent }).ToList();
                    }
                    calenderEvents.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));
                    calenderEvents.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

                    minDate = calenderEvents.Last().StartTime.AddHours(-1);
                    if (events.Items.Count < 50)
                    {
                        isNextExist = false;
                    }
                }
                else
                {
                    isNextExist = false;
                }
            }

            calenderEvents = calenderEvents.Distinct(new CalendarEventEqualyCompare()).ToList();

            return calenderEvents;
        }

    }

}
