using System.Configuration;

namespace MMS.Directory.Banks.WebApi
{
    public static class AppConfig
    {
        public static string ServiceUrl => ConfigurationManager.AppSettings["service:url"];
    }
}