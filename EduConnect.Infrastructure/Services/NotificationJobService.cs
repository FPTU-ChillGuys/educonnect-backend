using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace EduConnect.Infrastructure.Services
{
	public class NotificationJobService : INotificationJobService
	{
		private readonly IFcmNotificationService _fcmService;
		private readonly ILogger<NotificationJobService> _logger;

		public NotificationJobService(
			IFcmNotificationService fcmService,
			ILogger<NotificationJobService> logger)
		{
			_fcmService = fcmService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		/// Send test push message to simulate fire-and-forget job.
		/// </summary>
		public void SendTestNotification(string message, string deviceToken)
		{
			_logger.LogInformation("[Hangfire] Sending test notification: {Message}", message);

			// Optional: send to a test device token
			 _fcmService.SendNotificationAsync(deviceToken, "Test", message);
		}

		/// <summary>
		/// This method will be scheduled to send daily/weekly notifications.
		/// </summary>
		public void SendReportNotificationToParents(string message, string deviceToken)
		{
			_logger.LogInformation("[Hangfire] Sending synthesized RAG report notifications...");
			_fcmService.SendNotificationAsync(deviceToken, "Test", message);

			// TODO: Query RAG-generated reports from DB
			// var reports = _reportRepository.GetReportsForToday();

			// TODO: For each parent that needs to be notified
			// foreach (var report in reports)
			// {
			//     var deviceToken = report.ParentDeviceToken;
			//     var message = $"Report for {report.StudentName}: {report.Summary}";
			//     await _fcmService.SendNotificationAsync(deviceToken, "Student Report", message);
			// }
		}
	}
}
