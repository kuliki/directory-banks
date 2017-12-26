using MMS.Directory.Banks.Model;
using System.Collections.Generic;

namespace MMS.Directory.Banks.Core
{
    public interface IBankInfoStorage
    {
        IReadOnlyCollection<BankInfo> GetBanks(bool onlyActive);

        BankInfo GetBank(string bankOid);

        void UpdateBank(BankInfo bank);

        void Sync(IReadOnlyCollection<BankHeader> banks);

        //bool DeleteBank(string bankOid);
    }
}