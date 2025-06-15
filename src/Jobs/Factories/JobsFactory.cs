using Crypto.Monitoring.Services;
using Crypto.Monitoring.src.Enums;
using Hangfire;

namespace Crypto.Monitoring.src.Jobs.Factories
{
    public class JobsFactory
    {
        public static void CreateJob(string jobId, string cron, JobsEnum job)
        {
            switch (job)
            {
                case JobsEnum.CurrentBalanceMyCryptosJob:
                    RecurringJob.AddOrUpdate<CryptoService>(jobId, job => job.ConsultingBalance(), cron);
                    break;
                case JobsEnum.TopCryptoChange:
                    RecurringJob.AddOrUpdate<CryptoService>(jobId, job => job.GetTopCryptoChange(), cron);
                    break;
                case JobsEnum.Top10MarketCapCryptoChange:
                    RecurringJob.AddOrUpdate<CryptoService>(jobId, job => job.GetTopCryptoChangeMarketCap(), cron);
                    break;
            }
        }
    }
}
