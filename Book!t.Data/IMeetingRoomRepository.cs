using System.Collections.Generic;
using Bookit.Domain;

namespace Bookit.Data
{
    public interface IMeetingRoomRepository
    {
        IList<MeetingRoom> MeetingRooms
        {
            get;
        }

        MeetingRoom GetAt(int index);
    }
}
