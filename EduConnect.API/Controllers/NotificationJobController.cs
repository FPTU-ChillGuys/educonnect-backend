using EduConnect.Application.Interfaces.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationJobController : ControllerBase
	{
		private readonly IBackgroundJobClient _backgroundJobClient;
		private readonly IRecurringJobManager _recurringJobManager;

		public NotificationJobController(
			IBackgroundJobClient backgroundJobClient,
			IRecurringJobManager recurringJobManager)
		{
			_backgroundJobClient = backgroundJobClient;
			_recurringJobManager = recurringJobManager;
		}

		[HttpPost("fire-notify")]
		public IActionResult FireAndForgetTest([FromQuery] string message, string deviceToken)
		{
			_backgroundJobClient.Enqueue<INotificationJobService>(svc => svc.SendTestNotification(message, deviceToken));
			return Ok("Test notification job enqueued.");
		}

		[HttpPost("recurring-report")]
		public IActionResult ScheduleRecurringReport([FromQuery] string message, string deviceToken)
		{
			_recurringJobManager.AddOrUpdate<INotificationJobService>(
				"daily-report-job",
				svc => svc.SendReportNotificationToParents(message, deviceToken),
				//Cron.Daily); // Change to Cron.Weekly if needed
				Cron.Minutely());

			return Ok("Scheduled recurring report job.");
		}
	}
}
