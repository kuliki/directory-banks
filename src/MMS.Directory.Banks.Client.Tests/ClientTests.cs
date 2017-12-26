using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MMS.Directory.Banks.Client.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void BasicTest()
        {
            var client = new BanksClient(new BanksClientConfiguration
            {
                ApiUrl = "http://mow04ids002/banks/api"
            });

            var list = client.GetBankList(false);
            var bank = client.GetBank(list.First().Oid);

            client.UpdateBank(bank);
        }
    }
}
