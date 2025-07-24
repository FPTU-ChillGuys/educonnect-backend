using AutoMapper;
using EduConnect.Application.Commons.Extensions;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.Interfaces.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using Microsoft.SemanticKernel;
using System.Text;

namespace EduConnect.ChatbotAPI.Services.Student
{
    public class StudentReportServices : IStudentReportService
    {
        private readonly IReportService reportService;
        private readonly IClassSessionService classSessionService;
        private readonly IBehaviorService behaviorService;

        public StudentReportServices(
            IReportService reportService,
            IClassSessionService classSessionService,
            IBehaviorService behaviorService
        )
        {   
            this.reportService = reportService;
            this.classSessionService = classSessionService;
            this.behaviorService = behaviorService;
        }

        public async Task StudentReportDaily()
        {
            var vietnamNow = DateTimeHelper.GetVietnamTime();
            var startOfDay = vietnamNow.Date;
            var endOfDay = vietnamNow.Date.AddDays(1).AddTicks(-1);

            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(string.Empty, startOfDay, endOfDay);

            if (classSessions.Data == null) return;

            foreach (var classSession in classSessions.Data!)
            {
                var reportBuilder = new StringBuilder();

                var behaviorReport = await behaviorService.GetStudentBehaviorNotesAsync(classSession.ClassSessionId);

                if (behaviorReport.Data == null || !behaviorReport.Data.Any())
                {
                    reportBuilder.AppendLine("⚠️ Không có ghi chú hành vi nào.");
                }
                else
                {
                    foreach (var behavior in behaviorReport.Data!)
                    {
                        reportBuilder.AppendLine("=== HÀNH VI HỌC SINH ===");
                        reportBuilder.AppendLine($"Báo cáo được tạo vào: {vietnamNow:yyyy-MM-dd HH:mm}\n");

                        reportBuilder.AppendLine($"📚 Buổi học: {classSession.ClassName} - Ngày: {classSession.Date:yyyy-MM-dd}");
                        reportBuilder.AppendLine(new string('-', 50));

                        reportBuilder.AppendLine($"👤 Học sinh: {behavior.StudentFullName}");
                        reportBuilder.AppendLine($"📝 Ghi chú: {behavior.Comment}");
                        reportBuilder.AppendLine();

                        reportBuilder.AppendLine(new string('=', 50));
                        reportBuilder.AppendLine();

                        string fullReport = reportBuilder.ToString();

                        var studentReport = new CreateStudentReportRequest
                        {
                            StudentId = behavior.StudentId,
                            GeneratedByAI = false,
                            SummaryContent = fullReport,
                            StartDate = classSession.Date,
                            EndDate = classSession.Date.AddDays(1),
                        };

                        await reportService.CreateStudentReportAsync(studentReport);
                    }
                }
            }
        }

        public async Task StudentReportWeekly()
        {
            var vietnamNow = DateTimeHelper.GetVietnamTime();
            var startOfWeek = vietnamNow.AddDays(-7);
            var endOfWeek = vietnamNow;

            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(string.Empty, startOfWeek, endOfWeek);

            if (classSessions.Data == null) return;

            foreach (var classSession in classSessions.Data!)
            {
                var reportBuilder = new StringBuilder();

                var behaviorReport = await behaviorService.GetStudentBehaviorNotesAsync(classSession.ClassSessionId);

                if (behaviorReport.Data == null || !behaviorReport.Data.Any())
                {
                    reportBuilder.AppendLine("⚠️ Không có ghi chú hành vi nào.");
                }
                else
                {
                    foreach (var behavior in behaviorReport.Data!)
                    {
                        reportBuilder.AppendLine("=== HÀNH VI HỌC SINH ===");
                        reportBuilder.AppendLine($"Báo cáo được tạo vào: {vietnamNow:yyyy-MM-dd HH:mm}\n");

                        reportBuilder.AppendLine($"📚 Buổi học: {classSession.ClassName} - Ngày: {classSession.Date:yyyy-MM-dd}");
                        reportBuilder.AppendLine(new string('-', 50));

                        reportBuilder.AppendLine($"👤 Học sinh: {behavior.StudentFullName}");
                        reportBuilder.AppendLine($"📝 Ghi chú: {behavior.Comment}");
                        reportBuilder.AppendLine();

                        reportBuilder.AppendLine(new string('=', 50));
                        reportBuilder.AppendLine();

                        string fullReport = reportBuilder.ToString();

                        var studentReport = new CreateStudentReportRequest
                        {
                            StudentId = behavior.StudentId,
                            GeneratedByAI = false,
                            SummaryContent = fullReport,
                            StartDate = classSession.Date,
                            EndDate = classSession.Date.AddDays(1),
                        };

                        await reportService.CreateStudentReportAsync(studentReport);
                    }
                }
            }
        }
    }
}
