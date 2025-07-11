using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.DTOs.Responses.BehaviorResponses;
using EduConnect.Application.DTOs.Responses.SubjectResponses;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Services;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Plugins
{
    public class BehaviorPlugin(
            IBehaviorService behaviorService,
            IClassSessionService classSessionService
        )
    {

        [KernelFunction("GetClassBehaviorLogsByClassName")]
        [Description("Retrieves a list of class behavior logs for a specific session.")]
        public async Task<List<ClassBehaviorLogDto>> GetClassBehaviorLogs(string className)
        {
            var classSessions = await classSessionService.GetPagedClassSessionsAsync(
                new Application.DTOs.Requests.ClassSessionRequests.ClassSessionPagingRequest
                {
                    PageSize = 100,
                }
            );

            var classSession = classSessions.Data!
                .FirstOrDefault(cs => cs.ClassName!.Contains(className, StringComparison.OrdinalIgnoreCase));


            var behaviorLogs = new List<ClassBehaviorLogDto>();

            if (classSession != null)
            {
                var logs = await behaviorService.GetClassBehaviorLogsAsync(classSession.ClassSessionId);
                if (logs.Success && logs.Data != null)
                {
                    behaviorLogs.AddRange(logs.Data);
                }
            }
            return behaviorLogs;

        }

        [KernelFunction("GetStudentBehaviorNotesByStudentName")]
        [Description("Retrieves a list of student behavior notes for a specific session.")]
        public async Task<List<StudentBehaviorNoteDto>> GetStudentBehaviorNotes(string studentName)
        {
            var classSessions = await classSessionService.GetPagedClassSessionsAsync(
                new Application.DTOs.Requests.ClassSessionRequests.ClassSessionPagingRequest
                {
                    PageSize = 100,
                }
            );
            var classSession = classSessions.Data!
                .FirstOrDefault(cs => cs.ClassName!.Contains(studentName, StringComparison.OrdinalIgnoreCase));
            var behaviorNotes = new List<StudentBehaviorNoteDto>();
            if (classSession != null)
            {
                var notes = await behaviorService.GetStudentBehaviorNotesAsync(classSession.ClassSessionId);
                if (notes.Success && notes.Data != null)
                {
                    behaviorNotes.AddRange(notes.Data);
                }
            }
            return behaviorNotes;
        }

    }
}
