using System.Linq.Expressions;
using Crypto.Monitoring.Services;

namespace Crypto.Monitoring.Jobs
{
    public class HourJob
    {
        public static Expression<Action> Execute()
        {
            return () => Console.WriteLine("Job por hora em execução");
        }
    }
}