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

        [KernelFunction("GetPagedClassSessions")]
        [Description("Retrieves a paginated list of class sessions based on filters such as class ID or date range.")]
        public async Task<List<ClassSessionDto>> GetClassSessions()
        {
           
            var classSessions = await classSessionService.GetPagedClassSessionsAsync(
                new ClassSessionPagingRequest
                {
                    PageSize = 100
                }
            );
            return classSessions!.Data!.ToList();

        }

        [KernelFunction("GetClassTimetable")]
        [Description("Retrieves the class timetable for a given class within a date range.")]
        public async Task<List<TimetableViewDto>> GetClassTimetable(string className, DateTime from, DateTime to)
        {
            var result = await classService.GetPagedClassesAsync(
                   new ClassPagingRequest
                   {
                       PageSize = 100
                   }
               );

            var classes = result.Data!
                .Where(cs => cs.ClassName!.Contains(className, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var timeTableView = new List<TimetableViewDto>();

            for ( int i = 0; i < classes.Count; i++ )
            {
                var timetableView = await classSessionService.GetClassTimetableAsync(
                    classes[i].ClassId,
                    from,
                    to
                );

                if ( timetableView.Success && timetableView.Data != null )
                {
                    timeTableView.AddRange(timetableView.Data);
                }
            }

            return timeTableView;

        }

        //[KernelFunction("ExportClassTimetableToExcel")]
        //[Description("Exports the class timetable to Excel for a given class and date range.")]
        //public async Task<BaseResponse<byte[]>> ExportClassTimetableToExcel(Guid classId, DateTime from, DateTime to)
        //{
        //    // To be implemented
        //}

        //[KernelFunction("ExportTeacherTimetableToExcel")]
        //[Description("Exports the teacher's timetable to Excel for a given date range.")]
        //public async Task<BaseResponse<byte[]>> ExportTeacherTimetableToExcel(Guid teacherId, DateTime from, DateTime to)
        //{
        //    // To be implemented
        //}


    }
}
