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

        [KernelFunction("get_class_sessions_by_class_and_date_range")]
        [Description("Retrieves class sessions filtered by class name within a specified date range.")]
        public async Task<List<ClassSessionDto>> GetClassSessionsByClassName(string className, string fromDate, string toDate)
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

        [KernelFunction("get_today_class_sessions")]
        public async Task<List<ClassSessionDto>> GetClassSessionsTodayByClassName(string className)
        {

            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(className, DateTime.UtcNow, DateTime.UtcNow);

            if (classSessions.Data == null || classSessions.Data.Count == 0)
            {
                return new List<ClassSessionDto>();
            }
            return classSessions.Data
                .Where(cs => cs.ClassName!.Contains(className, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        [KernelFunction("get_weekly_class_sessions")]
        public async Task<List<ClassSessionDto>> GetClassSessionsWeeklyByClassName(string className)
        {

            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(className, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);

            if (classSessions.Data == null || classSessions.Data.Count == 0)
            {
                return new List<ClassSessionDto>();
            }
            return classSessions.Data
                .Where(cs => cs.ClassName!.Contains(className, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }



        [KernelFunction("get_class_sessions_by_teacher_and_date_range")]
        public async Task<List<ClassSessionDto>> GetClassSessionsByTeacherName(string teacherName, string fromDate, string toDate)
        {
            DateTime.TryParse(fromDate, out DateTime from);
            DateTime.TryParse(toDate, out DateTime to);

            var classSessions = await classSessionService.GetClassSessionsBySearchAsync(teacherName, from, to);

            if (classSessions.Data == null || classSessions.Data.Count == 0)
            {
                return new List<ClassSessionDto>();
            }
            return classSessions.Data
                .Where(cs => cs.TeacherName!.Contains(teacherName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }



        [KernelFunction("get_timetable_from_class_name")]
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

        [KernelFunction("get_timetable_from_teacher_name")]
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
