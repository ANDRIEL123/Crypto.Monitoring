using System.Linq.Expressions;
using Hangfire.Jobs.Enums;

namespace Hangfire.Jobs.Jobs
{
    public class JobsHelper
    {
        public static Expression<Action> GetJob(JobsEnum job)
        {
            switch (job)
            {
                case JobsEnum.Hour:
                    return HourJob.Execute();
                case JobsEnum.Minutely:
                    return MinutelyJob.Execute();
                case JobsEnum.Daily:
                    return DailyJob.Execute();
                default:
                    return () => Console.WriteLine("Default");
            }
        }

        public static string GetCron(JobsEnum job)
        {
            switch (job)
            {
                case JobsEnum.Hour:
                    return Cron.Hourly();
                case JobsEnum.Minutely:
                    return Cron.Minutely();
                case JobsEnum.Daily:
                    return Cron.Daily();
                default:
                    return string.Empty;
            }
        }
    }
}