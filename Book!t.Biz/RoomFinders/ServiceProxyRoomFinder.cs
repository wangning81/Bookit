using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Bookit.Biz.FreeBusyService;
using Bookit.Data;
using Bookit.Domain;
using log4net;
using Ninject;

namespace Bookit.Biz
{
    #region ProxyWrapper
    public class ProxyWrapper<T> where T : ICommunicationObject
    {
        private T _service;

        public ProxyWrapper()
        {
            _service = CreateNewInstance();
        }

        public void Invoke(Action<T> action)
        {
            try
            {
                action(_service);
            }
            catch (Exception)
            {
                if (_service.State != CommunicationState.Faulted)
                {
                    throw;
                }

                _service.Abort();
                _service = CreateNewInstance();

                action(_service);
            }
        }

        public TResult Invoke<TResult>(Func<T, TResult> func)
        {
            try
            {
                return func(_service);
            }
            catch (Exception)
            {
                if (_service.State != CommunicationState.Faulted)
                {
                    throw;
                }

                _service.Abort();
                _service = CreateNewInstance();

                return func(_service);
            }
        }

        private static T CreateNewInstance()
        {
            Type type = typeof(T);
            return (T)type.GetConstructor(Type.EmptyTypes).Invoke(null);
        }
    }
    #endregion

    public class ServiceProxyRoomFinder : IRoomFinder
    {
        private IMeetingRoomRepository roomRepository;
        private readonly List<string> roomEmails;
        private static ILog Log = LogManager.GetLogger(typeof(ServiceProxyRoomFinder));
        private static ProxyWrapper<FreeBusyServiceClient> client = new ProxyWrapper<FreeBusyServiceClient>();

        [Inject]
        public ServiceProxyRoomFinder(IMeetingRoomRepository rep)
        {
            roomRepository = rep;
            roomEmails = rep.MeetingRooms.Select(mr => mr.Email).ToList();
        }

        public virtual IList<SearchResult> Find(RegularSearchDetail detail)
        {
            IList<SearchResult> ret = new List<SearchResult>();
            for (int k = 0; k < roomEmails.Count; k++)
            {
                bool isFree = false;
                try
                {
                    isFree = client.Invoke<bool>(s => s.IsFree(roomEmails[k], detail.StartDateTime, detail.Duration));
                }
                catch (CommunicationException ce)
                {
                    Log.Error("Communication with service has failed, check if service is running!", ce);
                }
                catch (Exception e)
                {
                    Log.Error("Exception thrown when communicate with service", e);
                    throw;
                }

                if (isFree)
                {
                    ret.Add(
                                new SearchResult()
                                {
                                    TheRoom = roomRepository.GetAt(k)
                                }
                            );
                }
            }
            return ret;
        }

        public virtual IList<SearchResult> Find(OneClickSearchDetail detail)
        {
            IList<SearchResult> ret = new List<SearchResult>();
            for (int k = 0; k < roomEmails.Count; k++)
            {
                KeyValuePair<DateTime, int> resultPair = new KeyValuePair<DateTime, int>(DateTime.Now, -1);
                try
                {
                    resultPair = client.Invoke<KeyValuePair<DateTime, int>>(s => s.IsFreeForNext(roomEmails[k], detail.AvailableTolerenceThreshold, detail.StartDateTime));
                }
                catch (CommunicationException ce)
                {
                    Log.Error("Communication with service has failed, check if service is running!", ce);
                }
                catch (Exception e)
                {
                    Log.Error("Exception thrown when communicate with service", e);
                    throw;
                }

                if (resultPair.Value != -1)
                {
                    ret.Add(
                              new OneClickSearchResult()
                              {
                                  TheRoom = roomRepository.GetAt(k),
                                  AvailStartTime = resultPair.Key,
                                  AvailDuration = resultPair.Value
                              }
                          );
                }
            }
            return ret;
        }
    }
}
