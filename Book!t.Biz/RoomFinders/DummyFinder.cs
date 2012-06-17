using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookit.Domain;

namespace Bookit.Biz
{
    public class DummyFinder : IRoomFinder
    {
        #region IRoomFinder Members

        public IList<SearchResult> Find(RegularSearchDetail detail)
        {
            SearchResult sr1 = new SearchResult()
                                {
                                    TheRoom = new MeetingRoom
                                                {
                                                    Id = 1,
                                                    Capacity = 7,
                                                    Name = "Wu Ying Dian",
                                                    Email = "cnbjs.trainingexcell@thomsonreuters.com",
                                                    Description = "1F WS"
                                                },
                                    //AvailStartTime = DateTime.Now,
                                    //AvailEndTime = DateTime.Now.AddHours(2),
                                    Dist = 10,
                                    Score = 23.5
                                };

            SearchResult sr2 = new SearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 2,
                    Capacity = 10,
                    Name = "Wen Yuan Ge",
                    Email = "WenYuanGe@thomsonreuters.com",
                    Description = "2F ES"
                },
                //AvailStartTime = DateTime.Now,
                //AvailEndTime = DateTime.Now.AddHours(2),
                Dist = 10,
                Score = 35.5
            };

            SearchResult sr3 = new SearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Ti Ren Ge",
                    Email = "TiRenGe@thomsonreuters.com",
                    Description = "体仁阁"
                },
                //AvailStartTime = DateTime.Now,
                //AvailEndTime = DateTime.Now.AddHours(2),
                Dist = 10,
                Score = 35.5
            };

            SearchResult sr4 = new SearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Qian Long",
                    Email = "QianLong@thomsonreuters.com",
                    Description = "乾隆"
                },
                //AvailStartTime = DateTime.Now,
                //AvailEndTime = DateTime.Now.AddHours(2),
                Dist = 10,
                Score = 15.5
            };

            SearchResult sr5 = new SearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Yang Guifei",
                    Email = "YangGuiFei@thomsonreuters.com",
                    Description = "杨贵妃"
                },
                //AvailStartTime = DateTime.Now,
                //AvailEndTime = DateTime.Now.AddHours(2),
                Dist = 10,
                Score = 17.5
            };

            SearchResult sr6 = new SearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Meng Zi",
                    Email = "cnbjz.mencius@thomsonreuters.com",
                    Description = "孟子"
                },
                //AvailStartTime = DateTime.Now,
                //AvailEndTime = DateTime.Now.AddHours(2),
                Dist = 10,
                Score = 17.5
            };

            IList<SearchResult> ret = new List<SearchResult>();
            ret.Add(sr1);
            ret.Add(sr2);
            ret.Add(sr3);
            ret.Add(sr4);
            ret.Add(sr5);
            ret.Add(sr6);
            var arr = ret.ToArray();
            Array.Sort(arr);

            return new List<SearchResult>(arr);
        }

        #endregion

        public IList<SearchResult> Find(OneClickSearchDetail detail)
        {
            var sr1 = new OneClickSearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 1,
                    Capacity = 7,
                    Name = "Wu Ying Dian",
                    Email = "cnbjs.trainingexcell@thomsonreuters.com",
                    Description = "1F WS"
                },
                AvailStartTime = DateTime.Now,
                AvailDuration = 30,
                Dist = 10,
                Score = 23.5
            };

            var sr2 = new OneClickSearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 2,
                    Capacity = 10,
                    Name = "Wen Yuan Ge",
                    Email = "WenYuanGe@thomsonreuters.com",
                    Description = "2F ES"
                },
                AvailStartTime = DateTime.Now,
                AvailDuration = 25,
                Dist = 10,
                Score = 35.5
            };

            var sr3 = new OneClickSearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Ti Ren Ge",
                    Email = "TiRenGe@thomsonreuters.com",
                    Description = "体仁阁"
                },
                AvailStartTime = DateTime.Now,
                AvailDuration = 20,
                Dist = 10,
                Score = 35.5
            };

            var sr4 = new OneClickSearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Qian Long",
                    Email = "QianLong@thomsonreuters.com",
                    Description = "乾隆"
                },
                AvailStartTime = DateTime.Now,
                AvailDuration = 10,
                Dist = 10,
                Score = 15.5
            };

            var sr5 = new OneClickSearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Yang Guifei",
                    Email = "YangGuiFei@thomsonreuters.com",
                    Description = "杨贵妃"
                },
                AvailStartTime = DateTime.Now,
                AvailDuration = 15,
                Dist = 10,
                Score = 17.5
            };

            var sr6 = new OneClickSearchResult()
            {
                TheRoom = new MeetingRoom
                {
                    Id = 3,
                    Capacity = 10,
                    Name = "Meng Zi",
                    Email = "MengZi@thomsonreuters.com",
                    Description = "孟子"
                },
                AvailStartTime = DateTime.Now,
                AvailDuration = 20,
                Dist = 10,
                Score = 17.5
            };

            IList<OneClickSearchResult> ret = new List<OneClickSearchResult>();
            ret.Add(sr1);
            ret.Add(sr2);
            ret.Add(sr3);
            ret.Add(sr4);
            ret.Add(sr5);
            ret.Add(sr6);
            var arr = ret.ToArray();
            Array.Sort(arr);

            return new List<SearchResult>(arr);
        }
    }
}
