using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduConnect.Infrastructure.Services
{
	public class StartupNotificationJobScheduler : IHostedService
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public StartupNotificationJobScheduler(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceScopeFactory.CreateScope();
			var notificationJobService = scope.ServiceProvider.GetRequiredService<INotificationJobService>();

			notificationJobService.ScheduleDailyStudentReportJob();
			notificationJobService.ScheduleWeeklyStudentReportJob();
			notificationJobService.ScheduleWeeklyClassReportJob();

			await Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
