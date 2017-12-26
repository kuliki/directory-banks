using System.Configuration;

namespace MMS.Directory.Banks.Client
{
    public class BanksClientConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("apiUrl", IsRequired = true)]
        public string ApiUrl
        {
            get { return (string)base["apiUrl"]; }
            set { base["apiUrl"] = value; }
        }

        [ConfigurationProperty("timeout", DefaultValue = 30)]
        public int Timeout
        {
            get { return (int)base["timeout"]; }
            set { base["timeout"] = value; }
        }
    }
}