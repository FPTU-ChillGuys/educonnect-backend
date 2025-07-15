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

		[HttpGet]
		[Authorize(Roles = "admin,parent,teacher")]
		public async Task<IActionResult> GetPagedClasses([FromQuery] ClassPagingRequest request)
		{
			var result = await _classService.GetPagedClassesAsync(request);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpGet("lookup")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> GetClassLookup([FromQuery] ClassPagingRequest request)
		{
			var result = await _classService.GetClassLookupAsync(request);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpGet("{id}")]
		[Authorize(Policy = "ClassAccessPolicy")]
		public async Task<IActionResult> GetClassById(Guid id)
		{
			var result = await _classService.GetClassByIdAsync(id);
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
