using System.Collections.Generic;
using Bookit.Data;
using Bookit.Domain;
using log4net;
using Microsoft.Office.Interop.Outlook;
using Ninject;

namespace Bookit.Biz
{
    public class SimpleFinder : IRoomFinder
    {
        private readonly IMeetingRoomRepository roomRepository;
        private Application outlookApp;
        private NameSpace outlookNs;
        private IList<Recipient> recRooms = new List<Recipient>();
        private static ILog Log = LogManager.GetLogger(typeof(SimpleFinder));

        [Inject]
        public SimpleFinder(IMeetingRoomRepository rep)
        {
            outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            outlookNs = outlookApp.GetNamespace("MAPI");
            roomRepository = rep;

            foreach (MeetingRoom mr in roomRepository.MeetingRooms)
            {
                recRooms.Add(outlookNs.CreateRecipient(mr.Email));
            }
        }

        public virtual IList<SearchResult> Find(RegularSearchDetail detail)
        {
            IList<SearchResult> ret = new List<SearchResult>();
            for (int k = 0; k < recRooms.Count; k++)
            {
                var recipient = recRooms[k];
                
                string fbString = recipient.FreeBusy(detail.StartDateTime, 1);

                if (!string.IsNullOrEmpty(fbString))
                {
                    int startPoint = detail.StartDateTime.Hour * 60 + detail.StartDateTime.Minute;
                    int i = startPoint + 1;
                    while (i <= startPoint + detail.Duration && fbString[i] == '0')
                    {
                        i++;
                    }

                    if (i >= startPoint + detail.Duration)
                        ret.Add(
                                new SearchResult()
                                {
                                    TheRoom = roomRepository.GetAt(k)
                                }
                            );
                }
                else
                {
                    Log.Warn("FreeBusy returns null/empty string");
                }
            }
            return ret;
        }


        public virtual IList<SearchResult> Find(OneClickSearchDetail detail)
        {
            IList<SearchResult> ret = new List<SearchResult>();
            for (int k = 0; k < recRooms.Count; k++)
            {
                var recipient = recRooms[k];

                string fbString = recipient.FreeBusy(detail.StartDateTime, 1);

                int startPoint = detail.StartDateTime.Hour * 60 + detail.StartDateTime.Minute;
                int i = startPoint + 1;
                int latestAvailTime = startPoint + detail.AvailableTolerenceThreshold;

                while (i <= latestAvailTime && fbString[i] == '1')
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

                    ret.Add(
                            new OneClickSearchResult()
                            {
                                TheRoom = roomRepository.GetAt(k),
                                AvailStartTime = detail.StartDateTime.Date.AddMinutes(i),
                                AvailDuration = availDuration
                            }
                        );
                }
            }
            return ret;
        }
    }
}
