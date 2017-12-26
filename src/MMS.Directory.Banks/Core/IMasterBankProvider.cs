using MMS.Directory.Banks.Model;
using System.Collections.Generic;

namespace MMS.Directory.Banks.Core
{
    public interface IMasterBankProvider
    {
        IReadOnlyCollection<BankHeader> GetBankList();
    }
}
