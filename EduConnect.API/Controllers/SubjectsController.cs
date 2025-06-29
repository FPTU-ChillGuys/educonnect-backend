using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectsController : ControllerBase
	{
		private readonly ISubjectService _service;

		public SubjectsController(ISubjectService service)
		{
			_service = service;
		}

		[HttpGet]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> GetPagedSubject([FromQuery] SubjectPagingRequest request) {
			var response = await _service.GetPagedSubjectsAsync(request);
			return response.Success ? Ok(response) : BadRequest(response);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "admin,teacher,parent")]
		public async Task<IActionResult> GetById(Guid id) {
			var response = await _service.GetSubjectByIdAsync(id);
			return response.Success ? Ok(response) : NotFound(response);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Create(CreateSubjectRequest request) {
			var response = await _service.CreateSubjectAsync(request);
			return response.Success ? CreatedAtAction(nameof(GetById), new { id = response.Data }, response) : BadRequest(response.Errors);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Update(Guid id, UpdateSubjectRequest request){
			var response = await _service.UpdateSubjectAsync(id, request);
			return response.Success ? Ok(response) : BadRequest(response);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(Guid id){
			var response = await _service.DeleteSubjectAsync(id);
			return response.Success ? Ok(response) : BadRequest(response);
		}
	}
}
