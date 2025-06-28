using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BehaviorController : ControllerBase
	{
		private readonly IBehaviorService _behaviorService;

		public BehaviorController(IBehaviorService behaviorService)
		{
			_behaviorService = behaviorService;
		}

		[HttpGet("sessions/{sessionId}/class-logs")]
		public async Task<IActionResult> GetClassBehaviorLogs(Guid sessionId)
		{
			var response = await _behaviorService.GetClassBehaviorLogsAsync(sessionId);
			return response.Success ? Ok(response) : BadRequest(response);
		}

		[HttpGet("sessions/{sessionId}/student-notes")]
		public async Task<IActionResult> GetStudentBehaviorNotes(Guid sessionId)
		{
			var response = await _behaviorService.GetStudentBehaviorNotesAsync(sessionId);
			return response.Success ? Ok(response) : BadRequest(response);
		}

		[HttpPost("class-logs")]
		public async Task<IActionResult> CreateClassBehaviorLog([FromBody] CreateClassBehaviorLogRequest request)
		{
			var result = await _behaviorService.CreateClassBehaviorLogAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("student-notes")]
		public async Task<IActionResult> CreateStudentBehaviorNote([FromBody] CreateStudentBehaviorNoteRequest request)
		{
			var result = await _behaviorService.CreateStudentBehaviorNoteAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("class-logs/{logId}")]
		public async Task<IActionResult> UpdateClassBehaviorLog(Guid logId, [FromBody] UpdateClassBehaviorLogRequest request)
		{
			var result = await _behaviorService.UpdateClassBehaviorLogAsync(logId, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("student-notes/{noteId}")]
		public async Task<IActionResult> UpdateStudentBehaviorNote(Guid noteId, [FromBody] UpdateStudentBehaviorNoteRequest request)
		{
			var result = await _behaviorService.UpdateStudentBehaviorNoteAsync(noteId, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("class-logs/{logId}")]
		public async Task<IActionResult> DeleteClassBehaviorLog(Guid logId)
		{
			var result = await _behaviorService.DeleteClassBehaviorLogAsync(logId);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("student-notes/{noteId}")]
		public async Task<IActionResult> DeleteStudentBehaviorNote(Guid noteId)
		{
			var result = await _behaviorService.DeleteStudentBehaviorNoteAsync(noteId);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
