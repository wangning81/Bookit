using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Bookit.FreeBusyService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
                     Namespace = "http://thomonreuters.com/bookit")]
    public class RedemptionFreeBusyService : IFreeBusyService
    {
        private static RedemptionFreeBusyCache cache = RedemptionFreeBusyCache.Instance;
        public RedemptionFreeBusyService()
        {

        }

        public string GetFreeBusy(string email, DateTime stime, int duration)
        {
            return cache.GetFreeBusy(email, stime, duration);
        }

        public bool IsFree(string email, DateTime stime, int duration)
        {
            string fbString = cache.GetFreeBusy(email, stime, duration);
            if (!string.IsNullOrEmpty(fbString))
            {
                int startPoint = stime.Hour * 60 + stime.Minute;
                int i = startPoint + 1;
                while (i <= startPoint + duration && fbString[i] == '0')
                {
                    i++;
                }
                return i >= startPoint + duration;
            }
            return false;
        }

        public KeyValuePair<DateTime, int> IsFreeForNext(string email, int minutes, DateTime stime)
        {
            string fbString = cache.GetFreeBusy(email, stime, minutes + 60);
            if (!string.IsNullOrEmpty(fbString))
            {
                int startPoint = stime.Hour * 60 + stime.Minute;
                int i = startPoint + 1;
                int latestAvailTime = startPoint + minutes;

                while (i <= latestAvailTime && fbString[i] != '0')
                {
                    i++;
                }

                if (i <= latestAvailTime)
                {
                    int availDuration = 0;
                    int j = i;
                    while (fbString[j] == '0' && availDuration < 60)
                    {
                        availDuration++;
                        j++;
                    }
                    if (availDuration > 0)
                        return new KeyValuePair<DateTime, int>(stime.Date.AddMinutes(i), availDuration);
                }
            }
            return new KeyValuePair<DateTime,int>(new DateTime(), -1);
        }
    }
}
