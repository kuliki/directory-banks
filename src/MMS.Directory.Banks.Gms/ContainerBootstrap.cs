using Autofac;
using MMS.Directory.Banks.Core;

namespace MMS.Directory.Banks.Gms
{
    public static class ContainerBootstrap
    {
        public static void UseGms(this ContainerBuilder builder)
        {
            builder.RegisterType<GmsMasterBankProvider>().As<IMasterBankProvider>().InstancePerLifetimeScope();
        }
    }
}
