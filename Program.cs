using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoogleCalendarTestApp;
using Autofac;
using Google.Apis.Logging;


namespace CalendarQuickstart {
    class Program {

        static void Main(string[] args) {

            // Set up IoC container
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new GoogleAPIconnection())
                                        .As<IGoogleAPIconnection>();
            var container = builder.Build();

            // Run some code
            var apiConnection = container.Resolve<IGoogleAPIconnection>();
            var googleCalendar = new GoogleCalendar(apiConnection, "Google Calendar API .NET Quickstart");
            googleCalendar.GetEvents();

        }
    }
}