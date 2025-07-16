using EduConnect.Application.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Plugins
{
    public class ClassReportPlugin (
            ChatbotHelper chatbotHelper,
            ClassSessionService classSessionService,
            Kernel kernel
        )
    {

        public async Task ClassReportDaily()
        {

            string userPrompt = "Create a daily class report for all classes, including the number of students present, absent, and any notable events or issues that occurred during the day. Date: " + DateTime.Now.ToString("yyyy-MM-dd");

            //KernelFunction getClassSessionsFunction = kernel.Plugins.GetFunction("ClassSession", );





        }



    }
}
