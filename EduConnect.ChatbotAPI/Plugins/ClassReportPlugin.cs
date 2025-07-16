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

            //KernelFunction getClassSessionsByClassName = kernel.Plugins.GetFunction("ClassSession", "get_class_sessions_from_class_name_and_date");
            //KernelFunction getClassSessionsByTeacherName = kernel.Plugins.GetFunction("ClassSession", "get_class_sessions_from_teacher_name_and_date");
            //KernelFunction getTimetableByClassName = kernel.Plugins.GetFunction("ClassSession", "get_class_sessions_from_class_name");
            //KernelFunction getTimetableByTeacherName = kernel.Plugins.GetFunction("ClassSession", "get_class_sessions_from_teacher_name");

        }



    }
}
