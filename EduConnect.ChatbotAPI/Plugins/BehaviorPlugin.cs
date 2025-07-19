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

        [KernelFunction("get_class_behavior_logs_by_class_name")]
        [Description("Retrieves a list of class behavior logs for a specific session.")]
        public async Task<List<ClassBehaviorLogDto>> GetClassBehaviorLogs(string className)
        {
            var classBehaviorLogs = await behaviorService.GetClassBehaviorLogsBySearchAsync(className);
            return classBehaviorLogs.Data!.ToList();
        }

        [KernelFunction("get_student_behavior_notes_by_student_name")]
        [Description("Retrieves a list of student behavior notes for a specific session.")]
        public async Task<List<StudentBehaviorNoteDto>> GetStudentBehaviorNotes(string studentName)
        {
            var studentehaviorNotes = await behaviorService.GetStudentBehaviorNotesBySearchAsync(studentName);
            return studentehaviorNotes.Data!.ToList();
        }

    }
}
