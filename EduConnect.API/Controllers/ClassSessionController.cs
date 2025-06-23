using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using EduConnect.Application.Commons;
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

		[HttpGet("paged")]
		public async Task<IActionResult> GetPagedClassSessions([FromQuery] ClassSessionPagingRequest request)
		{
			var response = await _classSessionService.GetPagedClassSessionsAsync(request);
			return response.Success
				? Ok(response)
				: BadRequest(response.Errors);
		}

		[HttpGet("timetable/class/{classId}")]
		public async Task<IActionResult> GetClassTimetable(Guid classId, [FromQuery] DateTime from, [FromQuery] DateTime to)
		{
			var timetable = await _classSessionService.GetClassTimetableAsync(classId, from, to);
			return timetable.Success ? Ok(timetable) : BadRequest(timetable.Errors);
		}

		[HttpGet("timetable/teacher/{teacherId}")]
		public async Task<IActionResult> GetTeacherTimetable(Guid teacherId, [FromQuery] DateTime from, [FromQuery] DateTime to)
		{
			if (teacherId == Guid.Empty)
				return BadRequest("Invalid teacher ID");
			var timetable = await _classSessionService.GetClassTimetableAsync(teacherId, from, to);
			return timetable.Success ? Ok(timetable) : BadRequest(timetable.Errors);
		}

		[HttpGet("export/class/{classId}")]
		public async Task<IActionResult> ExportClassTimetable(Guid classId, DateTime from, DateTime to)
		{
			var result = await _classSessionService.ExportClassTimetableToExcelAsync(classId, from, to);
			return result.Success
				? File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ClassTimetable.xlsx")
				: BadRequest(result);
		}

		[HttpGet("export/teacher/{teacherId}")]
		public async Task<IActionResult> ExportTeacherTimetable(Guid teacherId, DateTime from, DateTime to)
		{
			var result = await _classSessionService.ExportTeacherTimetableToExcelAsync(teacherId, from, to);
			return result.Success
				? File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TeacherTimetable.xlsx")
				: BadRequest(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateSession([FromBody] CreateClassSessionRequest request)
		{
			var result = await _classSessionService.CreateClassSessionAsync(request);
			if (!result.Success)
				return BadRequest(result);

			return Ok(result);
		}

		[Authorize(Roles = "Teacher")]
		[HttpPut("teacher/{id}")]
		public async Task<IActionResult> UpdateClassSession(Guid id, [FromBody] UpdateClassSessionRequest request)
		{
			var teacherId = User.GetUserId(); // Assuming you have an extension method to extract user ID
			var result = await _classSessionService.UpdateClassSessionAsync(request, teacherId, id);

			return result.Success ? Ok(result) : BadRequest(result);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("admin/{id}")]
		public async Task<IActionResult> UpdateClassSessionByAdmin(Guid id, [FromBody] UpdateClassSessionByAdminRequest request)
		{
			var result = await _classSessionService.UpdateClassSessionByAdminAsync(request, id);
			return result.Success ? Ok(result) : BadRequest(result);
		}

	}
}
