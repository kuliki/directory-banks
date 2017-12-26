using MMS.Directory.Banks.Model;
using System.Threading.Tasks;

namespace MMS.Directory.Banks.Client
{
    public static class BanksClientSyncHelper
    {
        public static BankInfo[] GetBankList(this BanksClient client, bool onlyActive)
        {
            return Task.Run(() => client.GetBankListAsync(onlyActive).Result).Result;
        }

        public static BankInfo GetBank(this BanksClient client, string bankOid)
        {
            return Task.Run(() => client.GetBankAsync(bankOid).Result).Result;
        }

        public static void UpdateBank(this BanksClient client, BankInfo bank)
        {
            Task.Run(() => client.UpdateBankAsync(bank).GetAwaiter().GetResult()).GetAwaiter().GetResult();
        }

        public static void MasterSync(this BanksClient client)
        {
            Task.Run(() => client.MasterSyncAsync().GetAwaiter().GetResult()).GetAwaiter().GetResult();
        }
    }
}
