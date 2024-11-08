using System.Linq.Expressions;

namespace Hangfire.Jobs.Jobs
{
    public class DailyJob
    {
        public static Expression<Action> Execute()
        {
            return () => Console.WriteLine("Job diário em execução");
        }
    }
}