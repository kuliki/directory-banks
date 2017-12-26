using MMS.Directory.Banks.Client;
using Serilog;

namespace MMS.Directory.Banks.Sync.Jobs
{
    public class MasterSyncJob : JobBase
    {
        public MasterSyncJob(BanksClient client, ILogger logger)
            : base(logger)
        {
            Client = client;
        }

        protected override void ExecuteCore()
        {
            Client.MasterSync();
        }

        protected BanksClient Client { get; }
    }
}
