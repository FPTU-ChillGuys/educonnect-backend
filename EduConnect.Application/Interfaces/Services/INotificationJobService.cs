using EduConnect.Domain.Enums;

namespace EduConnect.Application.Interfaces.Services
{
	public interface INotificationJobService
	{
		Task SendStudentReportNotificationAsync(ReportType reportType);
		Task SendClassReportNotificationAsync(ReportType reportType);
		void ScheduleWeeklyStudentReportJob();
		void ScheduleWeeklyClassReportJob();
		void ScheduleDailyStudentReportJob();
		void ScheduleDailyClassReportJob();
	}
}
