using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.DTOs.Responses.StudentResponses;
using EduConnect.Application.Interfaces.Services;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Plugins
{
    public class StudentPlugin(
            IStudentService studentService
        )
    {

        //[KernelFunction("ExportStudentsToExcel")]
        //[Description("Exports student data to Excel using filters.")]
        //public async Task<BaseResponse<byte[]>> ExportStudentsToExcel(ExportStudentRequest request)
        //{
        //    // To be implemented
        //}

        [KernelFunction("get_all_students_from_class_name"), Description("Retrieves a list of students enrolled in a specific class.")]
        public async Task<List<StudentDto>> GetStudentsByClassName(string? className)
        {
            var students = await studentService.GetStudentsBySearchAsync(className!);

            return students!.Data!.Where(s => s.ClassName!.Contains(className!, StringComparison.OrdinalIgnoreCase)).ToList();
            //return students!.Data!.ToList();

        }

        [KernelFunction("get_students_from_parent_name")]
        [Description("Retrieves a list of students linked to a parent by their name.")]
        public async Task<List<StudentDto>> GetStudentsByParent(string parentName)  
        {
            var students = await studentService.GetStudentsBySearchAsync(parentName);
            return students!.Data!.Where(s => s.ParentFullName!.Contains(parentName, StringComparison.OrdinalIgnoreCase)).ToList();
            //return students!.Data!.ToList();
        }

        [KernelFunction("get_all_students")]
        [Description("Retrieves all students")]
        public async Task<List<StudentDto>> GetPagedStudents()
        {
            var students = await studentService.GetStudentsBySearchAsync(string.Empty);
            return students!.Data!.ToList();

        }

    }
}
