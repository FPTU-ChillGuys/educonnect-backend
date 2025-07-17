using EduConnect.ChatbotAPI.Services.Class;
using Hangfire;

namespace EduConnect.ChatbotAPI.Extensions
{
    public static class HangfireExtensions
    {
        public static void UseRegisteredHangfireJobs(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<ClassReportService>(
                "test_report_class_daily",
                services => services.ClassReportDaily(),
                Cron.Minutely
            );
        }
    }
}