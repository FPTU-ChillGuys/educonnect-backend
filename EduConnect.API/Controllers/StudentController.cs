using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService _studentService;

		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

		[HttpGet]
		[Authorize(Roles = "admin,parent,teacher")]
		public async Task<IActionResult> GetPagedStudents([FromQuery] StudentPagingRequest request)
		{
			var result = await _studentService.GetPagedStudentsAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
		{
			var result = await _studentService.CreateStudentAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("export")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> ExportStudentsToExcel([FromQuery] StudentPagingRequest request)
		{
			var result = await _studentService.ExportStudentsToExcelAsync(request);
			return result.Success
				? File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students.xlsx")
				: BadRequest(result);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequest request)
		{
			var result = await _studentService.UpdateStudentAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
