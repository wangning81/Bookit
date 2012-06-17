using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Bookit.FreeBusyService
{
    [ServiceContract]
    public interface IFreeBusyService
    {
        [OperationContract]
        string GetFreeBusy(string email, DateTime stime, int duration);
        [OperationContract]
        bool IsFree(string email, DateTime stime, int duration);
        [OperationContract]
        KeyValuePair<DateTime, int> IsFreeForNext(string email, int minutes, DateTime stime);
    }
}
