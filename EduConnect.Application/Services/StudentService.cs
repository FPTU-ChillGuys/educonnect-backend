using EduConnect.Application.DTOs.Responses.StudentResponses;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Extensions;
using EduConnect.Application.Commons.Dtos;
using Microsoft.EntityFrameworkCore;
using EduConnect.Domain.Entities;
using System.Linq.Expressions;
using FluentValidation;
using OfficeOpenXml;
using AutoMapper;

namespace EduConnect.Application.Services
{
	public class StudentService : IStudentService
	{
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Student> _studentRepo;
		private readonly IValidator<CreateStudentRequest> _validatorCreate;
		private readonly IValidator<UpdateStudentRequest> _validatorUpdate;
		private readonly IValidator<StudentPagingRequest> _studentPagingValidator;

		public StudentService(IValidator<StudentPagingRequest> studentPagingValidator, 
			IGenericRepository<Student> studentRepo, 
			IValidator<CreateStudentRequest> validatorCreate, 
			IValidator<UpdateStudentRequest> validatorUpdate,
			IMapper mapper)
		{
			_studentPagingValidator = studentPagingValidator;
			_validatorUpdate = validatorUpdate;	
			_validatorCreate = validatorCreate;
			_studentRepo = studentRepo;
			_mapper = mapper;
		}

		public async Task<BaseResponse<string>> UpdateStudentAsync(Guid id, UpdateStudentRequest request)
		{
			var student = await _studentRepo.GetByIdAsync(s => s.StudentId == id);
			if (student == null)
				return BaseResponse<string>.Fail("Student not found");

			var validationResult = await _validatorUpdate.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = validationResult.Errors.Select(e => e.ErrorMessage);
				return BaseResponse<string>.Fail(string.Join(" | ", errors));
			}

			_mapper.Map(request, student);
			_studentRepo.Update(student);

			var saved = await _studentRepo.SaveChangesAsync();
			return saved
				? BaseResponse<string>.Ok("Student updated successfully")
				: BaseResponse<string>.Fail("Failed to update student");
		}

		public async Task<BaseResponse<string>> CreateStudentAsync(CreateStudentRequest request)
		{
			var validationResult = await _validatorCreate.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
				return BaseResponse<string>.Fail(string.Join(" | ", errors));
			}

			var student = _mapper.Map<Student>(request);
			student.Status = "Active";
			await _studentRepo.AddAsync(student);
			var saved = await _studentRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Student created successfully")
				: BaseResponse<string>.Fail("Failed to create student");
		}

		public async Task<PagedResponse<StudentDto>> GetPagedStudentsAsync(StudentPagingRequest request)
		{
			var validationResult = await _studentPagingValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return PagedResponse<StudentDto>.Fail(errors, request.PageNumber, request.PageSize);
			}

			Expression<Func<Student, bool>> filter = s => true;

			if (!string.IsNullOrWhiteSpace(request.Keyword))
			{
				filter = filter.AndAlso(s =>
					s.FullName.Contains(request.Keyword) ||
					s.StudentCode!.Contains(request.Keyword));
			}

			if (request.ClassId.HasValue)
			{
				filter = filter.AndAlso(s => s.ClassId == request.ClassId.Value);
			}

			if (request.ParentId.HasValue)
			{
				filter = filter.AndAlso(s => s.ParentId == request.ParentId.Value);
			}

			if (!string.IsNullOrWhiteSpace(request.Status))
			{
				filter = filter.AndAlso(s => s.Status == request.Status);
			}

			if (!string.IsNullOrWhiteSpace(request.Gender))
			{
				filter = filter.AndAlso(s => s.Gender == request.Gender);
			}

			if (request.FromDate.HasValue)
			{
				Console.Write(String.Format("dd-mm-yyyy", request.FromDate));
				filter = filter.AndAlso(s => s.DateOfBirth >= request.FromDate.Value);
			}

			if (request.ToDate.HasValue)
			{
				Console.Write(String.Format("dd-mm-yyyy", request.ToDate));
				filter = filter.AndAlso(s => s.DateOfBirth <= request.ToDate.Value);
			}

			var (students, totalCount) = await _studentRepo.GetPagedAsync(
				filter: filter,
				include: q => q.Include(s => s.Class).Include(s => s.Parent),
				orderBy: q => q.ApplySorting(request.SortBy, request.SortDescending),
				pageNumber: request.PageNumber,
				pageSize: request.PageSize,
				asNoTracking: true
			);

			var dtoList = _mapper.Map<List<StudentDto>>(students);

			if (!dtoList.Any())
			{
				return PagedResponse<StudentDto>.Fail("No students found", request.PageNumber, request.PageSize);
			}

			return PagedResponse<StudentDto>.Ok(dtoList, totalCount, request.PageNumber, request.PageSize, "Students retrieved successfully");
		}

		public async Task<BaseResponse<byte[]>> ExportStudentsToExcelAsync(StudentPagingRequest request)
		{
			try
			{
				// Build dynamic filter using unified logic
				Expression<Func<Student, bool>> filter = s => true;

				if (!string.IsNullOrWhiteSpace(request.Keyword))
					filter = filter.AndAlso(s => s.FullName.Contains(request.Keyword) || s.StudentCode!.Contains(request.Keyword));

				if (request.ClassId.HasValue)
					filter = filter.AndAlso(s => s.ClassId == request.ClassId.Value);

				if (request.ParentId.HasValue)
					filter = filter.AndAlso(s => s.ParentId == request.ParentId.Value);

				if (!string.IsNullOrWhiteSpace(request.Status))
					filter = filter.AndAlso(s => s.Status == request.Status);

				if (!string.IsNullOrWhiteSpace(request.Gender))
					filter = filter.AndAlso(s => s.Gender == request.Gender);

				if (request.FromDate.HasValue)
					filter = filter.AndAlso(s => s.DateOfBirth >= request.FromDate.Value);

				if (request.ToDate.HasValue)
					filter = filter.AndAlso(s => s.DateOfBirth <= request.ToDate.Value);

				var students = await _studentRepo.GetAllAsync(
					filter: filter,
					include: q => q.Include(s => s.Class).Include(s => s.Parent),
					asNoTracking: true
				);

				if (!students.Any())
					return BaseResponse<byte[]>.Fail("No students found to export");

				// Excel export using EPPlus
				ExcelPackage.License.SetNonCommercialOrganization("EduConnect");
				using var package = new ExcelPackage();
				var worksheet = package.Workbook.Worksheets.Add("Students");

				// Header
				worksheet.Cells[1, 1].Value = "Student Code";
				worksheet.Cells[1, 2].Value = "Full Name";
				worksheet.Cells[1, 3].Value = "Date of Birth";
				worksheet.Cells[1, 4].Value = "Gender";
				worksheet.Cells[1, 5].Value = "Status";
				worksheet.Cells[1, 6].Value = "Class";
				worksheet.Cells[1, 7].Value = "Parent Email";
				worksheet.Cells[1, 8].Value = "Parent Name";
				worksheet.Cells[1, 9].Value = "Parent Phone";

				int row = 2;
				foreach (var s in students)
				{
					worksheet.Cells[row, 1].Value = s.StudentCode;
					worksheet.Cells[row, 2].Value = s.FullName;
					worksheet.Cells[row, 3].Value = s.DateOfBirth.ToString("yyyy-MM-dd");
					worksheet.Cells[row, 4].Value = s.Gender;
					worksheet.Cells[row, 5].Value = s.Status;
					worksheet.Cells[row, 6].Value = s.Class?.ClassName;
					worksheet.Cells[row, 7].Value = s.Parent?.Email;
					worksheet.Cells[row, 8].Value = s.Parent?.FullName;
					worksheet.Cells[row, 9].Value = s.Parent?.PhoneNumber;
					row++;
				}

				worksheet.Cells.AutoFitColumns();

				return BaseResponse<byte[]>.Ok(package.GetAsByteArray(), "Export successful");
			}
			catch (Exception ex)
			{
				// Log exception if needed
				return BaseResponse<byte[]>.Fail("An error occurred while exporting: " + ex.Message);
			}
		}

        public async Task<BaseResponse<List<StudentDto>>> GetStudentsBySearchAsync(string? search)
        {

			var students = await _studentRepo.GetAllAsync(
				filter: s => s.FullName.Contains(search!) || s.StudentCode!.Contains(search!),
				include: q => q.Include(s => s.Class).Include(s => s.Parent),
				asNoTracking: true
			);
			if (!students.Any())
			{
				return BaseResponse<List<StudentDto>>.Fail("No students found matching the search criteria");
			}
			var studentDtos = _mapper.Map<List<StudentDto>>(students);
			return BaseResponse<List<StudentDto>>.Ok(studentDtos, "Students retrieved successfully");
        }
    }
}
