using EduConnect.ChatbotAPI.Services.Class;
using Hangfire;

namespace EduConnect.ChatbotAPI.Extensions
{
    public static class HangfireExtensions
    {
        public static void UseRegisteredHangfireJobs(this IApplicationBuilder app, ClassReportService classReportPlugin)
        {
            RecurringJob.AddOrUpdate(
                "test_report_class_daily",
                () => classReportPlugin.ClassReportDaily(),
                Cron.Minutely
            );
        }
    }
}