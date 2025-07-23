using EduConnect.Application.DTOs.Requests.NotificationRequests;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using EduConnect.Domain.Enums;
using Hangfire;

namespace EduConnect.Infrastructure.Services;

public class NotificationJobService : INotificationJobService
{
	private readonly IFcmNotificationService _fcmService;
	private readonly IReportService _reportService;
	private readonly IUserService _userService;
	private readonly IClassRepository _classRepository;
	private readonly ILogger<NotificationJobService> _logger;
	private readonly IRecurringJobManager _recurringJobManager;
	private readonly INotificationService _notificationService;

	public NotificationJobService(
		IFcmNotificationService fcmService,
		IReportService reportService,
		IUserService userService,
		ILogger<NotificationJobService> logger,
		IRecurringJobManager recurringJobManager,
		IClassRepository classRepository,
		INotificationService notificationService)
	{
		_fcmService = fcmService;
		_reportService = reportService;
		_userService = userService;
		_logger = logger;
		_recurringJobManager = recurringJobManager;
		_classRepository = classRepository;
		_notificationService = notificationService;
	}

	public async Task SendStudentReportNotificationAsync(ReportType reportType)
	{
		_logger.LogInformation("[Hangfire] Sending {ReportType} student report notifications...", reportType);

		var parentTokenPairs = await _userService.GetAllParentDeviceTokensOfActiveStudentsAsync();

		if (parentTokenPairs == null || parentTokenPairs.Count == 0)
		{
			_logger.LogWarning("No parent device tokens found for active students.");
			return;
		}

		foreach (var (deviceToken, studentId, userId) in parentTokenPairs)
		{
			var reportResponse = await _reportService.GetLatestStudentReportForNotificationAsync(
				new GetStudentReportToNotifyRequest
				{
					StudentId = studentId,
					Type = reportType,
					GeneratedByAI = true
				});

			if (reportResponse.Data is not null)
			{
				var body = $"Student Report: {reportResponse.Data.SummaryContent}";

				_logger.LogInformation("Sending {ReportType} report for StudentId: {StudentId} to Token: {Token}",
					reportType, studentId, deviceToken);

				await _notificationService.CreateNotificationAsync(
					new CreateNotificationRequest
					{
						RecipientUserId = userId,
						StudentReportId = reportResponse.Data.ReportId,
						IsRead = false,
					});

				await _fcmService.SendNotificationAsync(deviceToken, $"{reportType} Report", body);
			}
		}
	}

	public async Task SendClassReportNotificationAsync(ReportType reportType)
	{
		_logger.LogInformation("[Hangfire] Sending {ReportType} class report notifications...", reportType);

		var parentTokenPairs = await _userService.GetAllParentDeviceTokensOfActiveStudentsAsync();
		var classParentMap = new Dictionary<Guid, List<(string DeviceToken, Guid UserId)>>();

		foreach (var (deviceToken, studentId, userId) in parentTokenPairs)
		{
			var classId = await _classRepository.GetClassIdByStudentIdAsync(studentId);
			if (classId == Guid.Empty) continue;

			if (!classParentMap.ContainsKey(classId))
				classParentMap[classId] = new List<(string, Guid)>();

			classParentMap[classId].Add((deviceToken, userId));
		}

		foreach (var (classId, parentList) in classParentMap)
		{
			var reportResponse = await _reportService.GetLatestClassReportForNotificationAsync(
				new GetClassReportToNotifyRequest
				{
					ClassId = classId,
					Type = reportType,
					GeneratedByAI = true
				});

			if (reportResponse.Data is not null)
			{
				var body = $"Class Report: {reportResponse.Data.SummaryContent}";

				foreach (var (deviceToken, userId) in parentList)
				{
					await _notificationService.CreateNotificationAsync(
						new CreateNotificationRequest
						{
							RecipientUserId = userId,
							ClassReportId = reportResponse.Data.ReportId,
							IsRead = false,
						});

					await _fcmService.SendNotificationAsync(deviceToken, $"{reportType} Class Report", body);
				}
			}
		}
	}

	public void ScheduleDailyStudentReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"send_daily_student_report",
			svc => svc.SendStudentReportNotificationAsync(ReportType.Daily),
			"0 1 * * *"); // 1:00 AM daily
	}

	public void ScheduleWeeklyStudentReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"send_weekly_student_report",
			svc => svc.SendStudentReportNotificationAsync(ReportType.Weekly),
			"0 1 * * 7"); // 1:00 AM Sunday
	}

	public void ScheduleWeeklyClassReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"send_weekly_class_report",
			svc => svc.SendClassReportNotificationAsync(ReportType.Weekly),
			"5 1 * * 7"); // 1:05 AM Sunday
	}
}
