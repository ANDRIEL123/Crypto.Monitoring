using System.Linq.Expressions;
using Crypto.Monitoring.Services;

namespace Crypto.Monitoring.Jobs
{
    public class MinutelyJob
    {
        public static Expression<Action> Execute()
        {
            return () => Console.WriteLine("Job por minuto em execução");
        }
    }
}