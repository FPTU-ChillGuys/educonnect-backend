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

        [KernelFunction("get_class_sessions")]
        [Description("Retrieves a list of classes with basic information such as class name, grade level, academic year, and homeroom teacher")]
        public async Task<List<ClassDto>> GetClassInfo()
        {
            var classes = await classService.GetClassesBySearchAsync(string.Empty);
            return classes.Data ?? new List<ClassDto>();
        }

        [KernelFunction("get_class_sessions_from_class_name")]
        [Description("Retrieves detailed information about a specific class by class name.")]
        public async Task<List<ClassDto>> GetClassByName(string className)
        {
            var classes = await classService.GetClassesBySearchAsync(className);

            if (classes.Data == null || classes.Data.Count == 0)
            {
                return new List<ClassDto>();
            }

            return classes.Data.Where(c => c.ClassName.Contains(className, StringComparison.OrdinalIgnoreCase)).ToList();

        }

        [KernelFunction("get_classes_from_teacher_name")]
        [Description("Retrieves a list of classes assigned to a specific teacher by their name.")]
        public async Task<List<ClassDto>> GetClassesByTeacherName(string teacherName)
        {
            var classes = await classService.GetClassesBySearchAsync(teacherName);

            if (classes.Data == null || classes.Data.Count == 0)
            {
                return new List<ClassDto>();
            }

            return classes.Data.Where(c => c.HomeroomTeacherName?.Contains(teacherName, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        }

    }
}
