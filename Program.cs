﻿using Google.Apis.Auth.OAuth2;
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

namespace CalendarQuickstart {
    class Program {

        static void Main(string[] args) {

            IGoogleAPIconnection apiConnection = new GoogleAPIconnection();
            var googleCalendar = new GoogleCalendar(apiConnection, "Google Calendar API .NET Quickstart");
            googleCalendar.GetEvents();

        }
    }
}