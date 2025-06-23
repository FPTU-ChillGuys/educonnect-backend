using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Interfaces.Services;
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

		[HttpGet("total")]
		public async Task<IActionResult> CountClassesAsync()
		{
			var response = await _classService.CountClassesAsync();
			return response.Success ? Ok(response) : BadRequest(response);	
		}

		[HttpPost]
		public async Task<IActionResult> CreateClass([FromBody] CreateClassRequest request)
		{
			var result = await _classService.CreateClassAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
