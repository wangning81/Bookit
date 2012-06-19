using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Bookit.Domain
{
    public class RegularSearchDetail : SearchDetail
    {
        public int AttendeeNumber { get; set; }

        public static RegularSearchDetail CreateDefault()
        {
            DateTime now = DateTime.Now;
            RegularSearchDetail model = new RegularSearchDetail()
            {
                StartDateTime = now.Minute < 30 ? now.AddMinutes(30 - now.Minute) : now.AddMinutes(60 - now.Minute),
                AttendeeNumber = 5,
                Duration = 60
            };
            return model;
        }

        public static RegularSearchDetail Create(string startTime,
                                                          string duration,
                                                          string attendees)
        {
            DateTime outStartTime;
            bool isValid = DateTime.TryParseExact(
                                        startTime,
                                        Constants.MeetingStartDateTimeUrlSchema,
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out outStartTime
                                        );

            int d;
            isValid &= int.TryParse(duration, out d);

            int at;
            isValid &= int.TryParse(attendees, out at);

            //start time must be later than now.
            isValid &= (outStartTime.CompareTo(DateTime.Now.AddMinutes(-10)) >= 0);

            if (!isValid
                || (d <= 0 || d > 600)
                || (at <= 0 || at > 30)
                )
                return null;

            return new RegularSearchDetail()
            {
                StartDateTime = outStartTime,
                AttendeeNumber = at,
                Duration = d
            };
        }
    }
}
