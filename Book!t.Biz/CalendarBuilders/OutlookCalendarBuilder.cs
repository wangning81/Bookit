using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using System.IO;

namespace Bookit.Biz
{
    public class OutlookCalendarBuilder : ICalendarFileBuilder
    {
        private Application olApp = new Application();
        private AppointmentItem appt;

        public OutlookCalendarBuilder()
        {
            
        }

        #region ICalendarFileBuilder Members

        public byte[] Build(DateTime startTime, int duration, string roomName, string roomEmail, out string ext)
        {
            appt = olApp.CreateItem(OlItemType.olAppointmentItem);
            appt.MeetingStatus = OlMeetingStatus.olMeeting;
            appt.Subject = "<subject goes here>";
            appt.Start = startTime;
            appt.Duration = duration;
            //appt.Location = "CN BJS (ARCA-1F, SE) Meeting Room Mencius [14] WB,TC,P";
            //appt.Recipients.Add(room_email);
            appt.ReminderSet = true;
            appt.Display(true);
            appt.Resources = roomEmail;
            

            var tempFile = Path.GetTempFileName();
            appt.SaveAs(tempFile);
            ext = "msg";
            return File.ReadAllBytes(tempFile);
        }

        #endregion
    }
}
