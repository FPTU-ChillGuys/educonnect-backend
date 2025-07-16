using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.DTOs.Responses.BehaviorResponses;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IBehaviorService
	{
		Task<BaseResponse<List<ClassBehaviorLogDto>>> GetClassBehaviorLogsAsync(Guid sessionId);
		Task<BaseResponse<List<StudentBehaviorNoteDto>>> GetStudentBehaviorNotesAsync(Guid sessionId);
		Task<BaseResponse<string>> CreateClassBehaviorLogAsync(CreateClassBehaviorLogRequest request);
		Task<BaseResponse<string>> CreateStudentBehaviorNoteAsync(CreateStudentBehaviorNoteRequest request);
		Task<BaseResponse<string>> UpdateClassBehaviorLogAsync(Guid logId, UpdateClassBehaviorLogRequest request);
		Task<BaseResponse<string>> UpdateStudentBehaviorNoteAsync(Guid noteId, UpdateStudentBehaviorNoteRequest request);
		Task<BaseResponse<string>> DeleteClassBehaviorLogAsync(Guid logId);
		Task<BaseResponse<string>> DeleteStudentBehaviorNoteAsync(Guid noteId);
	}
}
