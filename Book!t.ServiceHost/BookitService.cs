using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Bookit.FreeBusyService;
using System.Threading;
using log4net;

namespace Bookit.ServiceHost
{
    public partial class BookitService : ServiceBase
    {
        private System.ServiceModel.ServiceHost host;
        private ILog _log = LogManager.GetLogger(typeof(BookitService));

        public BookitService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {
            _log.Info("=========stopping WCF host=========");
            if (host != null)
            {
                try
                {
                    if (host.State == CommunicationState.Faulted)
                        host.Abort();
                }
                finally
                {
                    try
                    {
                        host.Close();
                    }
                    catch (Exception e)
                    {
                        _log.Warn("EX @ closing WCF host " + e);
                    }
                }
            }

            _log.Info("=========stopping cache=========");
            RedemptionFreeBusyCache.Instance.Stop();
            _log.Info("=========Book!t Service SHUT DOWN=========");
        }

        internal void Debug()
        {
            Start();
            AutoResetEvent ev = new AutoResetEvent(false);
            ev.WaitOne();
        }

        public void Start()
        {
            _log.Info("=========Book!t Service is starting=========");
            try
            {
                _log.Info("=========loading log4net configuration==========");
                log4net.Config.XmlConfigurator.Configure();
                
                _log.Info("===========initializing FreeBusyCache===========");
                RedemptionFreeBusyCache.Instance.Init();
                
                _log.Info("=============opening service host===============");
                host = new System.ServiceModel.ServiceHost(typeof(FreeBusyService.RedemptionFreeBusyService));
                host.Open();
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("EX @ BookitService.Start: {0}", ex);
                Environment.FailFast("EX @ BookitService.Start", ex);
            }

            _log.Info("=========Book!t Service is started=========");
        }
    }
}
