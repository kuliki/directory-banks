using Autofac;
using MMS.Directory.Banks.EntityFramework.Storage;
using MMS.Directory.Banks.Core;

namespace MMS.Directory.Banks.EntityFramework
{
    public static class Bootstrapper
    {
        public static void UseEntityFrameworkStorage(this ContainerBuilder builder)
        {
            builder.RegisterType<BankInfoStorage>().As<IBankInfoStorage>().InstancePerLifetimeScope();
        }
    }
}
