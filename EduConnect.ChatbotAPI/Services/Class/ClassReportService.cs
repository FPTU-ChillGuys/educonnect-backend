using AutoMapper;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.DTOs.Responses.ReportResponses;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.Domain.Entities;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Services.Class
{
    public class ClassReportService : IClassReportService
    {

        private readonly ChatbotHelper chatbotHelper;
        private readonly Kernel kernel;
        private readonly IReportService reportService;
        private readonly IMapper mapper;

        public ClassReportService(
            ChatbotHelper chatbotHelper,
            IReportService reportService,
            Kernel kernel
        )
        {
            this.chatbotHelper = chatbotHelper;
            this.kernel = kernel;
            this.reportService = reportService;
        }


        public async Task ClassReportDaily()
        {

            // var classess = await classService.GetClassesBySearchAsync(string.Empty);

            //foreach (var classItem in classess.Data)
            //{
            //    // Get class sessions for the class
            //    string userPrompt = $@"Get class session from class name {classItem.ClassName} and date range from {DateTime.UtcNow.ToString("yyyy-MM-dd")} to {DateTime.UtcNow.ToString("yyyy-MM-dd")}";

            //    GeminiPromptExecutionSettings geminiPromptExecutionSettings = new()
            //    {
            //        ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
            //    };

            //    var response = await kernel.InvokePromptAsync(userPrompt, new(geminiPromptExecutionSettings), cancellationToken: default);

            //}

            var className = "10C";
            var classId = "91F08E76-A26C-4A13-9777-00284FEEEE33";
            var dailyDate = DateTime.UtcNow;

            //Get class sessions for the class
            string userPrompt = $@"Get class session today from class has name {className}";

            var response = await chatbotHelper.ChatbotResponseNonStreaming(userPrompt);

            var classReport = new CreateClassReportRequest
            {
                ClassId = Guid.Parse(classId),
                StartDate = dailyDate,
                EndDate = dailyDate,
                GeneratedByAI = true,
                SummaryContent = response,
            };
            await reportService.CreateClassReportAsync(classReport);
        }



    }
}
