using System.Linq.Expressions;
using Crypto.Monitoring.Enums;
using Hangfire;

namespace Crypto.Monitoring.Jobs
{
    public class JobsHelper
    {
        [Obsolete]
        public static Expression<Action> GetJob(TypeJobsEnum job)
        {
            switch (job)
            {
                case TypeJobsEnum.Hour:
                    return HourJob.Execute();
                case TypeJobsEnum.Minutely:
                    return MinutelyJob.Execute();
                case TypeJobsEnum.Daily:
                    return DailyJob.Execute();
                default:
                    return () => Console.WriteLine("Default");
            }
        }

        public static string GetCron(TypeJobsEnum job)
        {
            switch (job)
            {
                case TypeJobsEnum.Hour:
                    return Cron.Hourly();
                case TypeJobsEnum.Minutely:
                    return Cron.Minutely();
                case TypeJobsEnum.Daily:
                    return Cron.Daily();
                default:
                    return string.Empty;
            }
        }
    }
}