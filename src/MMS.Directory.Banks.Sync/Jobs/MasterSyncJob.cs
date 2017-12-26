using MMS.Directory.Banks.Client;

namespace MMS.Directory.Banks.Sync.Jobs
{
    public class MasterSyncJob
    {
        public MasterSyncJob(BanksClient client)
        {
            Client = client;
        }

        public void Execute()
        {
            Client.MasterSync();
        }

        protected BanksClient Client { get; }
    }
}
