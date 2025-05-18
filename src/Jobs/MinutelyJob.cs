using System.Linq.Expressions;
using Crypto.Monitoring.Services;
using Crypto.Monitoring.src.Services;

namespace Hangfire.Jobs.Jobs
{
    public class MinutelyJob
    {
        public static Expression<Action> Execute()
        {
            return () => Task.Run(() => new CryptoService(new DiscordBotService()).ConsultingBalance());
        }
    }
}