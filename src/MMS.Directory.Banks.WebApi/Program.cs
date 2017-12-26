using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MMS.Directory.Banks.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Log.Logger = CreateLogger();

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
    }
}
