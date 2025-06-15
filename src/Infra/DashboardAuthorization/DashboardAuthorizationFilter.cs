using Hangfire.Dashboard;

namespace Crypto.Monitoring.Infra.DashboardAuthorization
{
    public class AllowAllDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}