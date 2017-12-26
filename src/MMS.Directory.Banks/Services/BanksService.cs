using MMS.Directory.Banks.Core;
using MMS.Directory.Banks.Model;
using System;
using System.Collections.Generic;

namespace MMS.Directory.Banks.Services
{
    public class BanksService
    {
        public BanksService(IBankInfoStorage storage, IMasterBankProvider masterBankProvider)
        {
            storage.AssertNotNull(nameof(storage));
            masterBankProvider.AssertNotNull(nameof(masterBankProvider));

            Storage = storage;
            MasterBankProvider = masterBankProvider;
        }

        public IReadOnlyCollection<BankInfo> GetBanks(bool onlyActive)
        {
            return Storage.GetBanks(onlyActive);
        }

        public BankInfo GetBank(string bankOid)
        {
            return Storage.GetBank(bankOid);
        }

        public void SaveBank(BankInfo bank)
        {
            Storage.UpdateBank(bank);
        }

        public void MasterSync()
        {
            var banks = MasterBankProvider.GetBankList();

            Storage.Sync(banks);
        }

        protected IBankInfoStorage Storage { get; }

        protected IMasterBankProvider MasterBankProvider { get; }
    }
}