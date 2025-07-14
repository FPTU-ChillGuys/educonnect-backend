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

		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> GetUsers([FromQuery] FilterUserRequest request)
		{
			var result = await _userService.GetPagedUsersAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet("lookup")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> GetUsersForSelectBox([FromQuery] FilterUserRequest request)
		{
			var result = await _userService.GetUserLookupAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var result = await _userService.GetUserByIdAsync(id);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost("export")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> ExportTeachersToExcel([FromQuery] FilterUserRequest request)
		{
			var result = await _userService.ExportUsersToExcelAsync(request);
			if (!result.Success)
				return BadRequest(result);

			return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Teachers.xlsx");
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
		{
			var result = await _userService.UpdateUserAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPatch("{id}/status")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateUserStatus(Guid id, [FromBody] UpdateUserStatusRequest request)
		{
			var result = await _userService.UpdateUserStatsusAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
