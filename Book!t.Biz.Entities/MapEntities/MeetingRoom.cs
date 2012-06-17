using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Domain
{
    [Flags]
    public enum Facilities
    {
        None,
        WhiteBoard,
        TeleConference,
        Projector
    }

    public class MeetingRoom : MapNode
    {
        public int Capacity
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string LocationDisplayName
        {
            get;
            set;
        }

        public string Facilities
        {
            get;
            set;
        }

        public bool IsBookable
        {
            get;
            set;
        }

        public string ReservedFeild1
        {
            get;
            set;
        }

        public string ReservedFeild2
        {
            get;
            set;
        }

        public string ReservedFeild3
        {
            get;
            set;
        }
    }
}
