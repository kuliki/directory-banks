using Autofac;
using MMS.Directory.Banks.Services;

namespace MMS.Directory.Banks
{
    public static class ContainerBootstrap
    {
        public static void UseBanksServices(this ContainerBuilder builder)
        {
            builder.RegisterType<BanksService>().As<BanksService>().InstancePerLifetimeScope();
        }
    }
}
