using EduConnect.Application.DTOs.Responses.ReportResponses;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Domain.Entities;
using FluentValidation;
using AutoMapper;

namespace EduConnect.Application.Services
{
	public class ReportService : IReportService
	{
		private readonly IGenericRepository<ClassReport> _classReportGenRepo;
		private readonly IGenericRepository<StudentReport> _studentReportGenRepo;
		private readonly IClassReportRepository _classReportRepo;
		private readonly IStudentReportRepository _studentReportRepo;
		private readonly IValidator<CreateClassReportRequest> _createClassReportValidator;
		private readonly IValidator<CreateStudentReportRequest> _createStudentReportRequestValidator;
		private readonly IMapper _mapper;

		public ReportService(
			IGenericRepository<ClassReport> classReportGenRepo,
			IMapper mapper,
			IClassReportRepository classReportRepo,
			IGenericRepository<StudentReport> studentReportGenRepo,
			IValidator<CreateClassReportRequest> createClassReportValidator,
			IValidator<CreateStudentReportRequest> createStudentReportRequestValidator,
			IStudentReportRepository studentReportRepo)
		{
			_classReportGenRepo = classReportGenRepo;
			_mapper = mapper;
			_classReportRepo = classReportRepo;
			_studentReportGenRepo = studentReportGenRepo;
			_createClassReportValidator = createClassReportValidator;
			_createStudentReportRequestValidator = createStudentReportRequestValidator;
			_studentReportRepo = studentReportRepo;
		}

		public async Task<BaseResponse<ClassReportDto>> GetLatestClassReportForNotificationAsync(GetClassReportToNotifyRequest request)
		{
			var latestReport = await _classReportRepo.GetLatestClassReportForNotificationAsync(request);

			if (latestReport == null)
				return BaseResponse<ClassReportDto>.Fail("No matching class report found");

			var dto = _mapper.Map<ClassReportDto>(latestReport);
			return BaseResponse<ClassReportDto>.Ok(dto, "Class report retrieved");
		}

		public async Task<BaseResponse<StudentReportDto>> GetLatestStudentReportForNotificationAsync(GetStudentReportToNotifyRequest request)
		{
			var latestReport = await _studentReportRepo.GetLatestStudentReportForNotificationAsync(request);

			if (latestReport == null)
				return BaseResponse<StudentReportDto>.Fail("No matching student report found");

			var dto = _mapper.Map<StudentReportDto>(latestReport);
			return BaseResponse<StudentReportDto>.Ok(dto, "Student report retrieved");
		}

		public async Task<BaseResponse<string>> CreateClassReportAsync(CreateClassReportRequest request)
		{
			var validationResult = await _createClassReportValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
				return BaseResponse<string>.Fail(string.Join(" | ", errors));
			}

			var entity = _mapper.Map<ClassReport>(request);
			await _classReportGenRepo.AddAsync(entity);
			var saved = await _classReportGenRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class report created successfully")
				: BaseResponse<string>.Fail("Failed to create class report");
		}

		public async Task<BaseResponse<string>> CreateStudentReportAsync(CreateStudentReportRequest request)
		{
			var validationResult = await _createStudentReportRequestValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = validationResult.Errors.Select(e => e.ErrorMessage);
				return BaseResponse<string>.Fail(string.Join(" | ", errors));
			}

			var entity = _mapper.Map<StudentReport>(request);
			await _studentReportGenRepo.AddAsync(entity);
			var saved = await _studentReportGenRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Student report created successfully")
				: BaseResponse<string>.Fail("Failed to create student report");
		}

		public async Task<BaseResponse<ClassReportDto>> GetClassReportByIdAsync(Guid classReportId)
		{
			var report = await _classReportGenRepo.GetByIdAsync(r => r.ReportId == classReportId);
			if (report == null)
				return BaseResponse<ClassReportDto>.Fail("Class report not found");

			var dto = _mapper.Map<ClassReportDto>(report);
			return BaseResponse<ClassReportDto>.Ok(dto, "Class report retrieved");
		}

		public async Task<BaseResponse<StudentReportDto>> GetStudentReportByIdAsync(Guid studentReportId)
		{
			var report = await _studentReportGenRepo.GetByIdAsync(r => r.ReportId == studentReportId);
			if (report == null)
				return BaseResponse<StudentReportDto>.Fail("Student report not found");

			var dto = _mapper.Map<StudentReportDto>(report);
			return BaseResponse<StudentReportDto>.Ok(dto, "Student report retrieved");
		}
	}
}
