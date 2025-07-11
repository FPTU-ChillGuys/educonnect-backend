using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.DTOs.Responses.ClassResponses;
using EduConnect.Application.Interfaces.Services;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Plugins
{
    public class ClassPlugin(
            IClassService classService,
            IClassSessionService classSessionService
        )
    {

        [KernelFunction("GetClassInfo")]
        [Description("Retrieves a list of classes with basic information such as class name, grade level, academic year, and homeroom teacher.")]
        public async Task<List<ClassDto>> GetClassInfo()
        {
            var classes = await classService.GetPagedClassesAsync(
                    new ClassPagingRequest
                    {
                        PageSize = 100
                    }
                );

            return classes.Data ?? new List<ClassDto>();
        }



    }
}
