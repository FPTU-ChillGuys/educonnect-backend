using EduConnect.ChatbotAPI.Services.Class;
using EduConnect.ChatbotAPI.Services.Student;
using Hangfire;

namespace EduConnect.ChatbotAPI.Extensions
{
    public static class HangfireExtensions
    {
		public static void UseRegisteredHangfireJobs(this IApplicationBuilder app)
        {
            // Create cron job per sunday at 0:00 AM
            RecurringJob.AddOrUpdate<ClassReportService>(
				"generate_weekly_class_report",
                service => service.ClassReportWeekly(),
                Cron.Weekly(DayOfWeek.Sunday, 0)
            );

            RecurringJob.AddOrUpdate<StudentReportServices>(
				"generate_daily_student_report",
                service => service.StudentReportDaily(),
                Cron.Daily
            );

            RecurringJob.AddOrUpdate<StudentReportServices>(
				"generate_weekly_student_report",
                service => service.StudentReportWeekly(),
                Cron.Weekly(DayOfWeek.Sunday, 0)
            );
        }
    }
}