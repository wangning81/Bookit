using System;
using System.Collections.Generic;
using System.Linq;
using Bookit.Domain;

namespace Bookit.Data
{
    public class MeetingRoomDBRepository : IMeetingRoomRepository, IDisposable
    {
        private BookitDB db = new BookitDB();
        private readonly IList<MeetingRoom> rooms;

        public MeetingRoomDBRepository()
        {
            rooms = db.MeetingRooms.ToList();
        }

        #region IMeetingRoomRepository Members

        public IList<MeetingRoom> MeetingRooms
        {
            get 
            {
                return rooms;
            }
        }

        public MeetingRoom GetAt(int index)
        {
            return rooms[index];
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
        }

        #endregion
    }
}
