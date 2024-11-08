using System.Linq.Expressions;

namespace Hangfire.Jobs.Jobs
{
    public class MinutelyJob
    {
        public static Expression<Action> Execute()
        {
            return () => Console.WriteLine("Job por minuto em execução 2");
        }
    }
}