using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookit.Biz
{
    public interface ICalendarFileBuilder
    {
        byte[] Build(
                        DateTime startTime,
                        int duration,
                        string roomName,
                        string roomEmail,
                        out string ext
                    );
    }
}
