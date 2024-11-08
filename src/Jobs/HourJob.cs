using System.Linq.Expressions;

namespace Hangfire.Jobs.Jobs
{
    public class HourJob
    {
        public static Expression<Action> Execute()
        {
            return () => Console.WriteLine("Job hora em execução");
        }
    }
}