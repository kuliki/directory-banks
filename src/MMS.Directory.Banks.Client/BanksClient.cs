using MMS.Directory.Banks.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Directory.Banks.Client
{
    public class BanksClient
    {
        public BanksClient(BanksClientConfiguration configuration)
        {
            configuration.AssertNotNull(nameof(configuration));

            Configuration = configuration;
        }

        public async Task<BankInfo[]> GetBankListAsync(bool onlyActive)
        {
            using (var client = CreateClient())
            {
                var requestUri = $"banks";

                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BankInfo[]>(json);
            }
        }

        public async Task<BankInfo> GetBankAsync(string bankOid)
        {
            using (var client = CreateClient())
            {
                var requestUri = $"banks/{bankOid}";

                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BankInfo>(json);
            }
        }

        public async Task UpdateBankAsync(BankInfo bank)
        {
            using (var client = CreateClient())
            {
                var requestUri = $"banks";

                var response = await client.PostAsync(requestUri, Serialize(bank));
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task MasterSyncAsync()
        {
            using (var client = CreateClient())
            {
                var requestUri = $"banks/sync";

                var response = await client.PostAsync(requestUri, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();
            }
        }

        private HttpContent Serialize<T>(T value)
        {
            var json = JsonConvert.SerializeObject(value);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private HttpClient CreateClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(Configuration.ApiUrl.EndsWith("\\") ? Configuration.ApiUrl : Configuration.ApiUrl + "\\"),
                Timeout = TimeSpan.FromSeconds(Configuration.Timeout)
            };
        }

        protected BanksClientConfiguration Configuration { get; }
    }
}
