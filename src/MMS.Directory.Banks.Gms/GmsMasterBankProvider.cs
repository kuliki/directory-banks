using MMS.Directory.Banks.Core;
using MMS.Directory.Banks.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MMS.Directory.Banks.Gms
{
    public class GmsMasterBankProvider : IMasterBankProvider
    {
        private string _referenceSapCode;

        public GmsMasterBankProvider()
        {
            _referenceSapCode = ConfigurationManager.AppSettings["reference:sapcode"];
        }

        public IReadOnlyCollection<BankHeader> GetBankList()
        {
            var client = new MMS.Integration.Gms.WebServices.Client.GmsClient(_referenceSapCode);
            var list = client.Reference.GetBankList();

            return list.Select(ToModel).ToArray();
        }

        private static BankHeader ToModel(MMS.Integration.Gms.WebServices.Interfaces.BankInfo from)
        {
            return new BankHeader
            {
                Oid = from.Oid,
                Name = from.BankName
            };
        }
    }
}