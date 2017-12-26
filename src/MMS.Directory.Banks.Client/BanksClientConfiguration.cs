using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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