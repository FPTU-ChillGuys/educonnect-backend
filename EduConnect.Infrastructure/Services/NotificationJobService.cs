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

	public NotificationJobService(
		IFcmNotificationService fcmService,
		IReportService reportService,
		IUserService userService,
		ILogger<NotificationJobService> logger,
		IRecurringJobManager recurringJobManager,
		IClassRepository classRepository)
	{
		_fcmService = fcmService;
		_reportService = reportService;
		_userService = userService;
		_logger = logger;
		_recurringJobManager = recurringJobManager;
		_classRepository = classRepository;
	}

	public async Task SendStudentReportNotificationAsync(ReportType reportType)
	{
		_logger.LogInformation("[Hangfire] Sending {ReportType} student report notifications...", reportType);

		// Get parent tokens linked to active students
		var parentTokenPairs = await _userService.GetAllParentDeviceTokensOfActiveStudentsAsync();

		if (parentTokenPairs == null || parentTokenPairs.Count == 0)
		{
			_logger.LogWarning("No parent device tokens found for active students.");
			return;
		}

		foreach (var (deviceToken, studentId) in parentTokenPairs)
		{
			var reportResponse = await _reportService.GetLatestStudentReportForNotificationAsync(
				new GetStudentReportToNotifyRequest
				{
					StudentId = studentId,
					Type = reportType,
					GeneratedByAI = true // optional filtering
				});

			if (reportResponse.Data is not null)
			{
				var body = $"Student Report: {reportResponse.Data.SummaryContent}";

				_logger.LogInformation("Sending {ReportType} report for StudentId: {StudentId} to Token: {Token}",
					reportType, studentId, deviceToken);

				await _fcmService.SendNotificationAsync(deviceToken, $"{reportType} Report", body);
			}
		}
	}

	public async Task SendClassReportNotificationAsync(ReportType reportType)
	{
		_logger.LogInformation("[Hangfire] Sending {ReportType} class report notifications...", reportType);

		// Get unique class IDs for active students
		var parentTokenPairs = await _userService.GetAllParentDeviceTokensOfActiveStudentsAsync();
		var uniqueClassMap = new Dictionary<Guid, List<string>>();

		foreach (var (deviceToken, studentId) in parentTokenPairs)
		{
			var classId = await _classRepository.GetClassIdByStudentIdAsync(studentId); // You implement this
			if (classId == Guid.Empty) continue;

			if (!uniqueClassMap.ContainsKey(classId))
				uniqueClassMap[classId] = new List<string>();

			uniqueClassMap[classId].Add(deviceToken);
		}

		foreach (var (classId, tokens) in uniqueClassMap)
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
				foreach (var token in tokens)
				{
					await _fcmService.SendNotificationAsync(token, $"{reportType} Class Report", body);
				}
			}
		}
	}

	public void ScheduleDailyStudentReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"daily-student-report-job",
			svc => svc.SendStudentReportNotificationAsync(ReportType.Daily),
			//"0 1 * * *"); // 1:00 AM daily
			"*/1 * * * *"); // 1 minute for test
	}

	public void ScheduleWeeklyStudentReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"weekly-student-report-job",
			svc => svc.SendStudentReportNotificationAsync(ReportType.Weekly),
			"0 1 * * 7"); // 1:00 AM Sunday
	}

	public void ScheduleDailyClassReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"daily-class-report-job",
			svc => svc.SendClassReportNotificationAsync(ReportType.Daily),
			"5 1 * * *"); // 1:05 AM daily
	}

	public void ScheduleWeeklyClassReportJob()
	{
		_recurringJobManager.AddOrUpdate<INotificationJobService>(
			"weekly-class-report-job",
			svc => svc.SendClassReportNotificationAsync(ReportType.Weekly),
			"5 1 * * 7"); // 1:05 AM Sunday
	}
}
