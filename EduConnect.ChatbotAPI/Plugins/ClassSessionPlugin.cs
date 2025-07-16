using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.DTOs.Responses.ClassSessionResponses;
using EduConnect.Application.Interfaces.Services;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Plugins
{
    public class ClassSessionPlugin
        (
            IClassSessionService classSessionService,
            IClassService classService
        )
    {

        [KernelFunction("get_class_sessions_from_class_name")]
        [Description("Retrieves a paginated list of class sessions based on filters such as class name or date range.")]
        public async Task<List<ClassSessionDto>> GetClassSessionsByNameAndDate(string className, string fromDate, string toDate)
        {
            DateTime.TryParse(fromDate, out DateTime from);
            DateTime.TryParse(toDate, out DateTime to);
           
            var classSessions = await classSessionService.GetClassSessionsBySearchAsync (className, from, to);

            if (classSessions.Data == null || classSessions.Data.Count == 0)
            {
                return new List<ClassSessionDto>();
            }
            return classSessions.Data
                .Where(cs => cs.ClassName!.Contains(className, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        [KernelFunction("get_class_sessions_from_class_name")]
        public async Task<List<TimetableViewDto>> GetTimetableByClassName(string className, string fromDate, string toDate)
        {
            DateTime.TryParse(fromDate, out DateTime from);
            DateTime.TryParse(toDate, out DateTime to);

            var timetableList = await classSessionService.GetTimetableViewBySearchAsync(className, from, to, Domain.Enums.TimetableSearchType.Class);

            if (timetableList.Data == null || timetableList.Data.Count == 0)
            {
                return new List<TimetableViewDto>();
            }
            
           return timetableList.Data.ToList();

        }

        [KernelFunction("get_class_sessions_from_teacher_name")]
        public async Task<List<TimetableViewDto>> GetTimetableByTeacherName(string className, string fromDate, string toDate)
        {
            DateTime.TryParse(fromDate, out DateTime from);
            DateTime.TryParse(toDate, out DateTime to);

            var timetableList = await classSessionService.GetTimetableViewBySearchAsync(className, from, to, Domain.Enums.TimetableSearchType.Teacher);

            if (timetableList.Data == null || timetableList.Data.Count == 0)
            {
                return new List<TimetableViewDto>();
            }

            return timetableList.Data.ToList();
        }

        //[KernelFunction("ExportClassTimetableToExcel")]
        //[Description("Exports the class timetable to Excel for a given class and date range.")]
        //public async Task<BaseResponse<byte[]>> ExportClassTimetableToExcel(Guid classId, DateTime from, DateTime to)
        //{
        //    // To be implemented
        //}

        //[KernelFunction("ExportTeacherTimetableToExcel")]
        //[Description("Exports the teacher's timetable to Excel for a given date range.")]
        //public async Task<BaseResponse<byte[]>> ExportTeacherTimetableToExcel(Guid teache     rId, DateTime from, DateTime to)
        //{
        //    // To be implemented
        //}


    }


}
