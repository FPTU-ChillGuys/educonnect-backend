using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<IActionResult> GetClassBehaviorLogs(Guid sessionId)
		{
			var response = await _behaviorService.GetClassBehaviorLogsAsync(sessionId);
			return response.Success ? Ok(response) : BadRequest(response);
		}

		[HttpGet("sessions/{sessionId}/student-notes")]
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<IActionResult> GetStudentBehaviorNotes(Guid sessionId)
		{
			var response = await _behaviorService.GetStudentBehaviorNotesAsync(sessionId);
			return response.Success ? Ok(response) : BadRequest(response);
		}

		[HttpPost("class-logs")]
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<IActionResult> CreateClassBehaviorLog([FromBody] CreateClassBehaviorLogRequest request)
		{
			var result = await _behaviorService.CreateClassBehaviorLogAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("student-notes")]
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<IActionResult> CreateStudentBehaviorNote([FromBody] CreateStudentBehaviorNoteRequest request)
		{
			var result = await _behaviorService.CreateStudentBehaviorNoteAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("class-logs/{logId}")]
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<IActionResult> UpdateClassBehaviorLog(Guid logId, [FromBody] UpdateClassBehaviorLogRequest request)
		{
			var result = await _behaviorService.UpdateClassBehaviorLogAsync(logId, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("student-notes/{noteId}")]
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<IActionResult> UpdateStudentBehaviorNote(Guid noteId, [FromBody] UpdateStudentBehaviorNoteRequest request)
		{
			var result = await _behaviorService.UpdateStudentBehaviorNoteAsync(noteId, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("class-logs/{logId}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteClassBehaviorLog(Guid logId)
		{
			var result = await _behaviorService.DeleteClassBehaviorLogAsync(logId);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("student-notes/{noteId}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteStudentBehaviorNote(Guid noteId)
		{
			var result = await _behaviorService.DeleteStudentBehaviorNoteAsync(noteId);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
