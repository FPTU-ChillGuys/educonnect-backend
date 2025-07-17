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
        private readonly IClassService classService;

        public ClassReportService(
            ChatbotHelper chatbotHelper,
            IReportService reportService,
            IClassService classService,
            Kernel kernel
        )
        {
            this.chatbotHelper = chatbotHelper;
            this.kernel = kernel;
            this.reportService = reportService;
            this.classService = classService;
        }


        public async Task ClassReportDaily()
        {

            var classess = await classService.GetClassesBySearchAsync(string.Empty);

            foreach (var classItem in classess!.Data!)
            {

                //Delay 10 minute per class to avoid rate limit
                await Task.Delay(TimeSpan.FromMinutes(5));

                //Get class sessions for the class
                string userPrompt = $@"Get today class session from class name {classItem.ClassName}";

                var response = await chatbotHelper.ChatbotResponseNonStreaming(userPrompt);

                var classReport = new CreateClassReportRequest
                {
                    ClassId = classItem.ClassId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    GeneratedByAI = true,
                    SummaryContent = response,
                };
                await reportService.CreateClassReportAsync(classReport);
            }
        }

        public async Task ClassReportWeekly()
        {
            var classess = await classService.GetClassesBySearchAsync(string.Empty);
            foreach (var classItem in classess!.Data!)
            {
                //Delay 10 minute per class to avoid rate limit
                await Task.Delay(TimeSpan.FromMinutes(5));
                //Get class sessions for the class
                string userPrompt = $@"Get weekly class session from class name {classItem.ClassName}";
                var response = await chatbotHelper.ChatbotResponseNonStreaming(userPrompt);
                var classReport = new CreateClassReportRequest
                {
                    ClassId = classItem.ClassId,
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now,
                    GeneratedByAI = true,
                    SummaryContent = response,
                };
                await reportService.CreateClassReportAsync(classReport);
            }
        }



    }
}
