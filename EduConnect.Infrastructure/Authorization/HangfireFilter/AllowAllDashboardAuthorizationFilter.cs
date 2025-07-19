using Hangfire.Dashboard;

namespace EduConnect.Infrastructure.Authorization.HangfireFilter
{
	public class AllowAllDashboardAuthorizationFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize(DashboardContext context) => true;
	}
}
