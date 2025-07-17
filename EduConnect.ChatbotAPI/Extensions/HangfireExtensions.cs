using EduConnect.ChatbotAPI.Services.Class;
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
        }
    }
}