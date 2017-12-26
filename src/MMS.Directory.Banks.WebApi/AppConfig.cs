using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Directory.Banks.WebApi
{
    public static class AppConfig
    {
        public static string ServiceUrl => ConfigurationManager.AppSettings["service:url"];
    }
}