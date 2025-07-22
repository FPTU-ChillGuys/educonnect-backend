using EduConnect.ChatbotAPI.Services.Class;
using EduConnect.ChatbotAPI.Services.Student;
using Hangfire;

namespace EduConnect.ChatbotAPI.Extensions
{
    public static class HangfireExtensions
    {
        public static void UseRegisteredHangfireJobs(this IApplicationBuilder app)
        {
            //RecurringJob.AddOrUpdate<ClassReportService>(
            //    "test_report_class_daily",
            //    services => services.ClassReportDaily(),
            //    Cron.Daily
            //);

            // Create cron job per sunday at 8:00 AM
            RecurringJob.AddOrUpdate<ClassReportService>(
                "class_report_daily",
                service => service.ClassReportWeekly(),
                Cron.Weekly(DayOfWeek.Sunday, 8)
            );

            RecurringJob.AddOrUpdate<StudentReportServices>(
                "student_report_daily",
                service => service.StudentReportDaily(),
                Cron.Daily
            );

            RecurringJob.AddOrUpdate<StudentReportServices>(
                "student_report_weekly",
                service => service.StudentReportWeekly(),
                Cron.Weekly(DayOfWeek.Sunday, 8)
            );
        }
    }
}