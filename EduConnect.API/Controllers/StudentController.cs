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
			_studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
		}

		[HttpGet("total")]
		public async Task<IActionResult> CountStudents()
		{
			var result = await _studentService.CountStudentsAsync();
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("export")]
		public async Task<IActionResult> ExportStudentsToExcel([FromBody] ExportStudentRequest request)
		{
			var result = await _studentService.ExportStudentsToExcelAsync(request);
			return result.Success
				? File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students.xlsx")
				: BadRequest(result);
		}

		[HttpGet("paged")]
		public async Task<IActionResult> GetPaged([FromQuery] StudentPagingRequest request)
		{
			var result = await _studentService.GetPagedStudentsAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
		{
			var result = await _studentService.CreateStudentAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[Authorize(Roles = "admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequest request)
		{
			var result = await _studentService.UpdateStudentAsync(id, request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
