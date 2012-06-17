using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using Bookit.Domain;

namespace Bookit.Data
{
    public class MeetingRoomFileRepository : IMeetingRoomRepository
    {
        private IList<MeetingRoom> rooms = new List<MeetingRoom>();
        private string FilePath = Path.Combine(
                                        AppDomain.CurrentDomain.BaseDirectory,
                                        ConfigurationManager.AppSettings["MeetingRoomDataFile"]);

        public IList<MeetingRoom> MeetingRooms
        {
            get
            {
                return rooms;
            }
        }

        public MeetingRoomFileRepository()
        {
            LoadFromFile();
        }

        public void LoadFromFile()
        {
            int id = 0;
            using (FileStream fs = File.OpenRead(FilePath))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(',');
                        rooms.Add(new MeetingRoom()
                                        {
                                            Id = id++,
                                            Capacity = int.Parse(tokens[0], CultureInfo.InvariantCulture),
                                            Description = tokens[1],
                                            Email = tokens[2],
                                            Name = tokens[3],
                                            Facilities = string.Empty,
                                            IsBookable = tokens[5].Trim() == "1"
                                        });
                    }
                }
            }
        }

        /// NOT SUPPORTED BY UI FOR NOW.
        /*
        private Facilities GetFacilitiesFrom(string s)
        {
            Facilities f = Facilities.WhiteBoard;
            foreach (string fs in s.Split(' '))
            {
                switch (fs.ToUpper())
                {
                    case "TC":
                        f |= Facilities.TeleConference;
                        break;
                    case "P":
                        f |= Facilities.Projector;
                        break;
                }
            }
            return f;
        }
         * */

        public MeetingRoom GetAt(int index)
        {
            return rooms[index];
        }
    }
}
