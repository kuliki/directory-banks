using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Directory.Banks.Sync.Jobs
{
    public abstract class JobBase
    {
        public JobBase(ILogger logger)
        {
            logger.AssertNotNull(nameof(logger));

            Logger = logger;
        }

        public void Execute()
        {
            try
            {
                Logger.Information($"Job '{GetType().Name}' started");
                ExecuteCore();
                Logger.Information($"Job '{GetType().Name}' completed");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Job '{GetType().Name}' error");
            }
        }

        protected abstract void ExecuteCore();

        protected ILogger Logger { get; }
    }
}
