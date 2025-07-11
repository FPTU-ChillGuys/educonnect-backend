using EduConnect.Application.Commons.Dtos;
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

        [KernelFunction("GetAllClassInfoExceptID")]
        [Description("Retrieves a list of classes with basic information such as class name, grade level, academic year, and homeroom teacher")]
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

        [KernelFunction("GetClassByNameExceptID")]
        [Description("Retrieves detailed information about a specific class by class name.")]
        public async Task<List<ClassDto>> GetClassByName(string name)
        {
            var classes = await classService.GetPagedClassesAsync(
                    new ClassPagingRequest
                    {
                        PageSize = 100,
                        Keyword = name
                    }
                );

            if (classes.Data == null || classes.Data.Count == 0)
            {
                return new List<ClassDto>();
            }
            return classes.Data.Where(c => c.ClassName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();


        }

        [KernelFunction("GetClassesByTeacherNameExceptID")]
        [Description("Retrieves a list of classes assigned to a specific teacher by their name.")]
        public async Task<List<ClassDto>> GetClassesByTeacherName(string teacherName)
        {
            var classes = await classService.GetPagedClassesAsync(
                    new ClassPagingRequest
                    {
                        PageSize = 100,
                    }
                );
            if (classes.Data == null || classes.Data.Count == 0)
            {
                return new List<ClassDto>();
            }
            return classes.Data.Where(c => c.HomeroomTeacherName?.Contains(teacherName, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        }

     






    }
}
