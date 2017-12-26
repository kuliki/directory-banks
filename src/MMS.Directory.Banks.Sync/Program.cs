using Serilog;
using Topshelf;

namespace MMS.Directory.Banks.Sync
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = CreateLogger();

            HostFactory.Run(x =>
            {
                x.UseSerilog();
                x.Service<HostService>();
                x.RunAsLocalSystem();
                x.SetDisplayName("MMS.Directory.Banks.Sync");
                x.SetServiceName("MMS.Directory.Banks.Sync");
            });
        }

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                 .ReadFrom.AppSettings()
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
