using System.Linq.Expressions;

namespace Crypto.Monitoring.Jobs
{
    public class DailyJob
    {
        public static Expression<Action> Execute()
        {
            return () => Console.WriteLine("Job diário em execução");
        }
    }
}