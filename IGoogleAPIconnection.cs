using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace GoogleCalendarTestApp
{
    public interface IGoogleAPIconnection
    {
        UserCredential GetToken();

        CalendarService CreateCalendarService(UserCredential credential, string applicationName);
    }
}
