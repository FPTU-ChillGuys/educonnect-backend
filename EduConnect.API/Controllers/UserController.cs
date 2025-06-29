using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("count")]
		[Authorize]
		public async Task<IActionResult> CountUsers()
		{
			var result = await _userService.CountTeachersAsync();
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet("count/homeroom-teachers")]
		[Authorize]
		public async Task<IActionResult> CountHomeroomTeachers()
		{
			var result = await _userService.CountHomeroomTeachersAsync();
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet("count/subject-teachers")]
		[Authorize]
		public async Task<IActionResult> CountSubjectTeachers()
		{
			var result = await _userService.CountSubjectTeachersAsync();
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetTeacher([FromQuery] FilterUserRequest request)
		{
			var result = await _userService.GetPagedUsersAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet("export")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ExportTeachersToExcel([FromQuery] ExportUserRequest request)
		{
			var result = await _userService.ExportUsersToExcelAsync(request);
			if (!result.Success)
				return BadRequest(result);

			return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Teachers.xlsx");
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin,Parent,Teacher")]
		public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
		{
			var result = await _userService.UpdateUserAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPatch("{id}/status")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateUserStatus(Guid id, [FromBody] UpdateUserStatusRequest request)
		{
			var result = await _userService.UpdateUserStatsusAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
