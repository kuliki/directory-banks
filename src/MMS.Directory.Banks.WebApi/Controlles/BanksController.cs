using MMS.Directory.Banks.Model;
using MMS.Directory.Banks.Services;
using MMS.Directory.Banks.WebApi.Filters;
using System.Web.Http;
using System.Web.Http.Description;

namespace MMS.Directory.Banks.WebApi.Controlles
{
    [RoutePrefix("api/banks")]
    [ErrorHandler, ActionLoggerHandler]
    public class BanksController : ApiController
    {
        public BanksController(BanksService banksService)
        {
            BanksService = banksService;
        }

        [HttpGet, Route]
        [ResponseType(typeof(BankInfo[]))]
        public IHttpActionResult GetBankList(bool onlyActive = false)
        {
            var list = BanksService.GetBanks(onlyActive);

            return Ok(list);
        }

        [HttpGet, Route("{bankOid}")]
        [ResponseType(typeof(BankInfo))]
        public IHttpActionResult GetBank(string bankOid)
        {
            var bank = BanksService.GetBank(bankOid);

            return Ok(bank);
        }

        [HttpPost, Route]
        public IHttpActionResult SaveBank(BankInfo bank)
        {
            BanksService.SaveBank(bank);

            return Ok();
        }

        [HttpPost, Route("sync")]
        public IHttpActionResult Sync()
        {
            BanksService.MasterSync();

            return Ok();
        }

        protected BanksService BanksService { get; }
    }
}
