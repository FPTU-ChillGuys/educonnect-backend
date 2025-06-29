using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassController : ControllerBase
	{
		private readonly IClassService _classService;
		public ClassController(IClassService classService)
		{
			_classService = classService;
		}

		[HttpGet("count")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> CountClassesAsync()
		{
			var response = await _classService.CountClassesAsync();
			return response.Success ? Ok(response) : BadRequest(response);	
		}

		[HttpGet("{id}")]
		[Authorize(Policy = "ClassAccessPolicy")]
		public async Task<IActionResult> GetClassById(Guid id)
		{
			var result = await _classService.GetClassByIdAsync(id);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpGet("homeroom-teacher/{teacherId}")]
		[Authorize(Roles = "admin,teacher")]
		public async Task<IActionResult> GetClassesByTeacherId(Guid teacherId)
		{
			var result = await _classService.GetClassesByTeacherIdAsync(teacherId);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpGet("student/{studentId}")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> GetClassesByStudentId(Guid studentId)
		{
			var result = await _classService.GetClassesByStudentIdAsync(studentId);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateClass([FromBody] CreateClassRequest request)
		{
			var result = await _classService.CreateClassAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateClass(Guid id, [FromBody] UpdateClassRequest request)
		{
			var result = await _classService.UpdateClassAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteClass(Guid id)
		{
			var result = await _classService.DeleteClassAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
