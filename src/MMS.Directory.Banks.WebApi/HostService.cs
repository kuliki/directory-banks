using Microsoft.Owin.Hosting;
using System;
using Topshelf;

namespace MMS.Directory.Banks.WebApi
{
    public class HostService : ServiceControl
    {
        private IDisposable _webApplication;

        public bool Start(HostControl hostControl)
        {
            _webApplication = WebApp.Start<Startup>(AppConfig.ServiceUrl);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _webApplication.Dispose();

            return true;
        }
    }
}