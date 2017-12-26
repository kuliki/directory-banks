using Serilog;
using System;
using Topshelf;

namespace MMS.Directory.Banks.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Log.Logger = CreateLogger();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            HostFactory.Run(x =>
            {
                x.UseSerilog();
                x.Service<HostService>();
                x.RunAsLocalSystem();
                x.SetDisplayName("MMS.Directory.Banks.WebApi");
                x.SetServiceName("MMS.Directory.Banks.WebApi");
            });
        }

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration().ReadFrom.AppSettings()
                    .Enrich.FromLogContext()
                    .CreateLogger();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
                Log.Logger.Fatal($"UnhandledException from '{sender}': {e.ExceptionObject.ToString()}");
            else
                Log.Logger.Fatal($"UnhandledException");
        }
    }
}
