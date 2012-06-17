using System;
using System.Globalization;
using System.Text;

namespace Bookit.Biz
{
    public class VCalendarBuilder : ICalendarFileBuilder
    {
        public static string BuildContent(DateTime startTime,
                                    int duration,
                                    string roomName,
                                    string roomEmail
                                   )
        {
            StringBuilder sbICSFile = new StringBuilder();
            DateTime dtNow = DateTime.Now;

            DateTime s = startTime;
            DateTime e = s.AddMinutes(duration);

            sbICSFile.AppendLine("BEGIN:VCALENDAR");
            sbICSFile.AppendLine("VERSION:2.0");
            sbICSFile.AppendLine("PRODID:-//Thomson Reuters//Book!t//EN");

            sbICSFile.AppendLine("BEGIN:VEVENT");
            sbICSFile.AppendLine("ATTENDEE;CUTYPE=RESOURCE;ROLE=NON-PARTICIPANT;RSVP=TRUE:mailto:" + roomEmail);
            sbICSFile.AppendLine("CLASS:PUBLIC");

            sbICSFile.Append("DTSTART:");
            sbICSFile.AppendLine(ToTimeString(s));

            sbICSFile.Append("DTEND:");
            sbICSFile.AppendLine(ToTimeString(e));

            sbICSFile.Append("LOCATION:");
            sbICSFile.AppendLine(roomName);

            sbICSFile.AppendLine("SUMMARY:");
            sbICSFile.AppendLine("TRANSP:OPAQUE");
            sbICSFile.AppendLine("UID:" + Guid.NewGuid().ToString("N") + "@book!t");

            StringBuilder sbDesc = new StringBuilder();
            for (int i = 0; i < 5; i++)
                sbDesc.Append(@"\n");
            sbDesc.Append(@"---------------------------------------\n\n");
            sbDesc.Append(@"<This meeting room is booked by Book!t>\n");

            sbICSFile.AppendLine("DESCRIPTION: " + sbDesc.ToString());
            //sbICSFile.AppendLine("ORGANIZER;");
            sbICSFile.AppendLine("PRIORITY:5");
            sbICSFile.AppendLine("SEQUENCE:0");

            sbICSFile.Append("DTSTAMP:" + dtNow.Year.ToString(CultureInfo.InvariantCulture));
            sbICSFile.Append(ToTimeString(dtNow));

            sbICSFile.AppendLine("BEGIN:VALARM");
            sbICSFile.AppendLine("TRIGGER:-PT15M");
            sbICSFile.AppendLine("ACTION:DISPLAY");
            sbICSFile.AppendLine("DESCRIPTION:Reminder");
            sbICSFile.AppendLine("END:VALARM");

            sbICSFile.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:BUSY");
            sbICSFile.AppendLine("X-MICROSOFT-CDO-IMPORTANCE:1");
            sbICSFile.AppendLine("X-MS-OLK-AUTOFILLLOCATION:TRUE");


            sbICSFile.AppendLine("END:VEVENT");
            sbICSFile.AppendLine("END:VCALENDAR");

            return sbICSFile.ToString();
        }

        private static string ToTimeString(DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dt.Year.ToString(CultureInfo.InvariantCulture));
            sb.Append(FormatDateTimeValue(dt.Month));
            sb.Append(FormatDateTimeValue(dt.Day) + "T");
            sb.Append(FormatDateTimeValue(dt.Hour));
            sb.Append(FormatDateTimeValue(dt.Minute) + "00");
            return sb.ToString();
        }

        private static string FormatDateTimeValue(int DateValue)
        {
            if (DateValue < 10)
                return "0" + DateValue.ToString(CultureInfo.InvariantCulture);
            else
                return DateValue.ToString(CultureInfo.InvariantCulture);
        }

        #region ICalendarFileBuilder Members

        public byte[] Build(DateTime startTime, int duration, string roomName, string roomEmail, out string ext)
        {
            var content = BuildContent(startTime, duration, roomName, roomEmail);
            ext = "ics";
            return ASCIIEncoding.ASCII.GetBytes(content);
        }

        #endregion
    }
}
