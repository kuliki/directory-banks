using Autofac;
using Hangfire.MemoryStorage;
using Hangfire;
using Owin;
using System.Web.Http;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using MMS.Directory.Banks.Sync.Jobs;
using System.Configuration;
using MMS.Directory.Banks.Client;

namespace MMS.Directory.Banks.Sync
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BuildContainer();
            var config = new HttpConfiguration();
            
            config.Routes.MapHttpRoute("default", "{controller}/{action}");

            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            GlobalConfiguration.Configuration.UseMemoryStorage();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions { Authorization = new[] { new HangFireAuthorizationFilter() } });

            config.EnsureInitialized();
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance((BanksClientConfiguration)ConfigurationManager.GetSection("banksClient"));
            builder.RegisterType<BanksClient>().InstancePerLifetimeScope();

            builder.RegisterType<MasterSyncJob>();

            return builder.Build();
        }

        private class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize([NotNull] DashboardContext context)
            {
                return true;
            }
        }
    }
}