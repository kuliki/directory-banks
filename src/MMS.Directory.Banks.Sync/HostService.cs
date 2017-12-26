using Hangfire;
using Microsoft.Owin.Hosting;
using MMS.Directory.Banks.Sync.Jobs;
using System;
using Topshelf;

namespace MMS.Directory.Banks.Sync
{
    public class HostService : ServiceControl
    {
        private IDisposable _webApplication;
        private BackgroundJobServer _jobServer;

        public bool Start(HostControl hostControl)
        {
            _webApplication = WebApp.Start<Startup>(AppConfig.ServiceUrl);
            _jobServer = new BackgroundJobServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1
            });

            RecurringJob.AddOrUpdate<MasterSyncJob>(x => x.Execute(), Cron.MinuteInterval(10));

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _webApplication.Dispose();
            _jobServer.Dispose();

            return true;
        }
    }
}