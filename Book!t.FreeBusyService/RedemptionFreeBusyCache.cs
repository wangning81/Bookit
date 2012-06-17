using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Bookit.Data;
using log4net;
using Redemption;

namespace Bookit.FreeBusyService
{
    public class RedemptionFreeBusyCache : IDisposable
    {
        private IList<string> emails;
        private RDOSession rdoSession;
        private bool initilized = false;
        private Dictionary<string, KeyValuePair<DateTime, string>> cache = new Dictionary<string, KeyValuePair<DateTime, string>>();
        private ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
        private Thread poolingThread;
        private static readonly ILog Log = LogManager.GetLogger(typeof(RedemptionFreeBusyCache));

        public readonly static RedemptionFreeBusyCache Instance = new RedemptionFreeBusyCache();

        private RedemptionFreeBusyCache()
        {

        }

        public void Init()
        {
            if (!initilized)
            {
                Log.Info("begin initialising cache...");

                this.rdoSession = new RDOSession();
                rdoSession.Logon();

                BookitDB db = new BookitDB();
                this.emails = db.MeetingRooms.Select(mr => mr.Email).ToList();

                poolingThread = new Thread(PoolingWorker);
                poolingThread.IsBackground = true;
                poolingThread.Start();

                initilized = true;

                Log.Info("cache initialized");
            }
        }

        void PoolingWorker()
        {
            while (true)
            {
                Log.Info("cache updating begins");
                foreach (var e in emails)
                {
                    try
                    {
                        var mname = rdoSession.AddressBook.ResolveName(e);
                        DateTime nowaDay = DateTime.Now.Date;
                        string fb = mname.GetFreeBusy(nowaDay, 1);

                        rw.EnterWriteLock();
                        try
                        {
                            cache[e] = new KeyValuePair<DateTime, string>(nowaDay, fb);
                        }
                        finally
                        {
                            rw.ExitWriteLock();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn("EX @ " + "PoolingWorker " + ex);
                    }
                }
                Log.Info("cache updating finished.");
            }
        }

        public string GetFreeBusy(string email, DateTime stime, int duration)
        {
            DateTime sdate = stime.Date;
            DateTime edate = stime.AddMinutes(duration);

            rw.EnterReadLock();
            try
            {
                if (cache.ContainsKey(email))
                {
                    var entry = cache[email];
                    DateTime baseDate = entry.Key;

                    if (baseDate <= sdate && edate <= baseDate.AddDays(30))
                    {
                        string baseFb = entry.Value;
                        TimeSpan diff = sdate - baseDate;
                        string ret = baseFb.Substring((int)diff.TotalMinutes);
                        return ret;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("EX @ geting free/busy from RedemptionFBCache: " + e);
            }
            finally
            {
                rw.ExitReadLock();
            }

            Log.WarnFormat("RedemptionCached missed for s = {0}, e = {1}", sdate, edate);

            try
            {
                var mname = rdoSession.AddressBook.ResolveName(email);
                var fb = mname.GetFreeBusy(sdate, 1);
                return fb;
            }
            catch (Exception e)
            {
                Log.Error("EX @ after cache missed then get free/busy from Exchange: " + e);
            }

            return null;
        }

        public void Stop()
        {
            if (poolingThread != null)
            {
                try
                {
                    poolingThread.Abort();
                }
                catch (Exception ex)
                {
                    Log.Warn("EX @ stopping poolingThread: " + ex);
                }
            }

            if (rw != null)
            {
                try
                {
                    rw.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Warn("EX @ disposing ReaderWriterLock " + ex);
                }
            }

            if (rdoSession != null)
            {
                try
                {
                    rdoSession.Logoff();
                }
                catch (Exception ex)
                {
                    Log.Warn("EX @ logging off RDOSession " + ex);
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                Stop();
            }
        }

        #endregion
    }
}
