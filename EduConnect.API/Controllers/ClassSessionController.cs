using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassSessionController : ControllerBase
	{
		private readonly IClassSessionService _classSessionService;
		public ClassSessionController(IClassSessionService classSessionService)
		{
			_classSessionService = classSessionService;
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> GetPagedClassSessions([FromQuery] ClassSessionPagingRequest request)
		{
			var response = await _classSessionService.GetPagedClassSessionsAsync(request);
			return response.Success
				? Ok(response)
				: NotFound(response);
		}

		[HttpGet("timetable")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> GetTimetable([FromQuery] TimetableRequest request)
		{
			var result = await _classSessionService.GetTimetableAsync(request);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost("export/timetable")]
		[Authorize(Roles = "admin,teacher")]
		public async Task<IActionResult> ExportTeacherTimetable([FromQuery] TimetableRequest request)
		{
			var result = await _classSessionService.ExportTimetableToExcelAsync(request);
			return result.Success
				? File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Timetable.xlsx")
				: BadRequest(result);
		}

		[HttpPost]
		[Authorize(Roles = "admin,teacher")]
		public async Task<IActionResult> CreateSession([FromBody] CreateClassSessionRequest request)
		{
			var result = await _classSessionService.CreateClassSessionAsync(request);
			if (!result.Success)
				return BadRequest(result);

			return Ok(result);
		}

		[HttpPut("teacher/{sessionId}")]
		[Authorize(Roles = "admin,teacher")]
		public async Task<IActionResult> UpdateClassSession(Guid sessionId, [FromBody] UpdateClassSessionRequest request)
		{
			var teacherId = User.GetUserId(); // Assuming you have an extension method to extract user ID
			var result = await _classSessionService.UpdateClassSessionAsync(request, teacherId, sessionId);

			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("admin/{sessionId}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateClassSessionByAdmin(Guid sessionId, [FromBody] UpdateClassSessionByAdminRequest request)
		{
			var result = await _classSessionService.UpdateClassSessionByAdminAsync(request, sessionId);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin,teacher")]
		public async Task<IActionResult> SoftDeleteClassSession(Guid id)
		{
			var result = await _classSessionService.SoftDeleteClassSessionAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("hard/{id}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteClassSession(Guid id)
		{
			var result = await _classSessionService.DeleteClassSessionAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
