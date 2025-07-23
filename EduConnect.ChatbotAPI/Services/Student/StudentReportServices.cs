using AutoMapper;
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
            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(string.Empty, DateTime.Now, DateTime.Now);

            if (classSessions.Data == null) return;

            foreach (var classSession in classSessions.Data!)
            {
                var reportBuilder = new StringBuilder();

                var behaviorReport = await behaviorService.GetStudentBehaviorNotesAsync(classSession.ClassSessionId);

                if (behaviorReport.Data == null || !behaviorReport.Data.Any())
                {
                    reportBuilder.AppendLine("⚠️ No behavior notes available.");
                }
                else
                {
                    foreach (var behavior in behaviorReport.Data!)
                    {
                        reportBuilder.AppendLine("=== STUDENT BEHAVIOR ===");
                        reportBuilder.AppendLine($"Report generated on: {DateTime.Now:yyyy-MM-dd HH:mm}\n");

                        reportBuilder.AppendLine($"📚 Class Session: {classSession.ClassName} - Date: {classSession.Date:yyyy-MM-dd}");
                        reportBuilder.AppendLine(new string('-', 50));

                        reportBuilder.AppendLine($"👤 Student: {behavior.StudentFullName}");
                        reportBuilder.AppendLine($"📝 Notes: {behavior.Comment}");
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
            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(string.Empty, DateTime.Now.AddDays(-7), DateTime.Now);

            if (classSessions.Data == null) return;

            foreach (var classSession in classSessions.Data!)
            {
                var reportBuilder = new StringBuilder();

                var behaviorReport = await behaviorService.GetStudentBehaviorNotesAsync(classSession.ClassSessionId);

                if (behaviorReport.Data == null || !behaviorReport.Data.Any())
                {
                    reportBuilder.AppendLine("⚠️ No behavior notes available.");
                }
                else
                {
                    foreach (var behavior in behaviorReport.Data!)
                    {
                        reportBuilder.AppendLine("=== STUDENT BEHAVIOR ===");
                        reportBuilder.AppendLine($"Report generated on: {DateTime.Now:yyyy-MM-dd HH:mm}\n");

                        reportBuilder.AppendLine($"📚 Class Session: {classSession.ClassName} - Date: {classSession.Date:yyyy-MM-dd}");
                        reportBuilder.AppendLine(new string('-', 50));

                        reportBuilder.AppendLine($"👤 Student: {behavior.StudentFullName}");
                        reportBuilder.AppendLine($"📝 Notes: {behavior.Comment}");
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
