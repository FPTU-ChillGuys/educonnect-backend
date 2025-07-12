using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.DTOs.Responses.ClassSessionResponses;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Extensions;
using EduConnect.Application.Commons.Dtos;
using Microsoft.EntityFrameworkCore;
using EduConnect.Domain.Entities;
using System.Linq.Expressions;
using System.Globalization;
using FluentValidation;
using OfficeOpenXml;
using AutoMapper;

namespace EduConnect.Application.Services
{
	public class ClassSessionService : IClassSessionService
	{
		private readonly IGenericRepository<ClassSession> _classSessionRepo;
		private readonly IValidator<CreateClassSessionRequest> _validatorCreate;
		private readonly IValidator<UpdateClassSessionRequest> _validatorUpdate;
		private readonly IValidator<UpdateClassSessionByAdminRequest> _validatorUpdateByAdmin;
		private readonly IMapper _mapper;
		public ClassSessionService(IGenericRepository<ClassSession> classSessionRepo,
			IValidator<CreateClassSessionRequest> validatorCreate,
			IValidator<UpdateClassSessionRequest> validatorUpdate,
			IValidator<UpdateClassSessionByAdminRequest> validatorUpdateByAdmin,
			IMapper mapper)
		{
			_classSessionRepo = classSessionRepo;
			_validatorCreate = validatorCreate;
			_validatorUpdate = validatorUpdate;
			_validatorUpdateByAdmin = validatorUpdateByAdmin;
			_mapper = mapper;
		}

		public async Task<BaseResponse<List<TimetableViewDto>>> GetClassTimetableAsync(Guid classId, DateTime from, DateTime to)
		{
			try
			{
				if (classId == Guid.Empty)
					return BaseResponse<List<TimetableViewDto>>.Fail("Invalid class ID");

				// If no date range provided, fetch all sessions for the class
				bool hasDateRange = from != default && to != default;
				if (hasDateRange && from > to)
					return BaseResponse<List<TimetableViewDto>>.Fail("From date must be earlier than To date");

				Expression<Func<ClassSession, bool>> filter = cs => cs.ClassId == classId;
				if (hasDateRange)
					filter = filter.AndAlso(cs => cs.Date >= from && cs.Date <= to);

				filter = filter.AndAlso(cs => !cs.IsDeleted);

				var sessions = await _classSessionRepo.GetAllAsync(
					filter: filter,
					include: q => q
						.Include(cs => cs.Subject)
						.Include(cs => cs.Teacher)
						.Include(cs => cs.Class),
					asNoTracking: true
				);

				if (!sessions.Any())
					return BaseResponse<List<TimetableViewDto>>.Fail("No class sessions found");

				var grouped = sessions
						.GroupBy(cs => cs.Date.Date)
						.OrderBy(g => g.Key)
						.Select(g => new TimetableViewDto
						{
							Date = g.Key,
							DayOfWeek = g.Key.ToString("dddd", new CultureInfo("vi-VN")),
							Periods = g.OrderBy(cs => cs.PeriodNumber)
								.Select(cs => new PeriodSlotDto
								{
									PeriodNumber = cs.PeriodNumber,
									ClassName = cs.Class?.ClassName ?? "N/A",
									SubjectName = cs.Subject?.SubjectName ?? "N/A",
									TeacherName = cs.Teacher?.UserName ?? "N/A",
									LessonContent = cs.LessonContent
								}).ToList()
						}).ToList();

				return BaseResponse<List<TimetableViewDto>>.Ok(grouped);
			}
			catch (Exception ex)
			{
				return BaseResponse<List<TimetableViewDto>>.Fail($"An error occurred while retrieving timetable: {ex.Message}");
			}
		}

		public async Task<BaseResponse<List<TimetableViewDto>>> GetTeacherTimetableAsync(Guid teacherId, DateTime from, DateTime to)
		{
			try
			{
				if (teacherId == Guid.Empty)
					return BaseResponse<List<TimetableViewDto>>.Fail("Invalid teacher ID");

				// Optional date range handling
				bool hasDateRange = from != default && to != default;
				if (hasDateRange && from > to)
					return BaseResponse<List<TimetableViewDto>>.Fail("From date must be earlier than To date");

				Expression<Func<ClassSession, bool>> filter = cs => cs.TeacherId == teacherId;
				if (hasDateRange)
					filter = filter.AndAlso(cs => cs.Date >= from && cs.Date <= to);

				filter = filter.AndAlso(cs => !cs.IsDeleted);

				var sessions = await _classSessionRepo.GetAllAsync(
					filter: filter,
					include: q => q
						.Include(cs => cs.Subject)
						.Include(cs => cs.Class)
						.Include(cs => cs.Teacher),
					asNoTracking: true
				);

				if (!sessions.Any())
					return BaseResponse<List<TimetableViewDto>>.Fail("No sessions found for this teacher");

				var grouped = sessions
						.GroupBy(cs => cs.Date.Date)
						.OrderBy(g => g.Key)
						.Select(g => new TimetableViewDto
						{
							Date = g.Key,
							DayOfWeek = g.Key.ToString("dddd", new CultureInfo("vi-VN")),
							Periods = g.OrderBy(cs => cs.PeriodNumber)
								.Select(cs => new PeriodSlotDto
								{
									PeriodNumber = cs.PeriodNumber,
									ClassName = cs.Class?.ClassName ?? "N/A",
									SubjectName = cs.Subject?.SubjectName ?? "N/A",
									TeacherName = cs.Teacher?.UserName ?? "N/A",
									LessonContent = cs.LessonContent
								}).ToList()
						}).ToList();

				return BaseResponse<List<TimetableViewDto>>.Ok(grouped, "Teacher timetable loaded successfully");
			}
			catch (Exception ex)
			{
				return BaseResponse<List<TimetableViewDto>>.Fail($"An error occurred while retrieving timetable: {ex.Message}");
			}
		}

		public async Task<PagedResponse<ClassSessionDto>> GetPagedClassSessionsAsync(ClassSessionPagingRequest request)
		{
			Expression<Func<ClassSession, bool>> filter = s => true;

			if (request.ClassId.HasValue)
				filter = filter.AndAlso(s => s.ClassId == request.ClassId.Value);

			if (request.SubjectId.HasValue)
				filter = filter.AndAlso(s => s.SubjectId == request.SubjectId.Value);

			if (request.TeacherId.HasValue)
				filter = filter.AndAlso(s => s.TeacherId == request.TeacherId.Value);

			if (request.FromDate.HasValue)
				filter = filter.AndAlso(s => s.Date >= request.FromDate.Value);

			if (request.ToDate.HasValue)
				filter = filter.AndAlso(s => s.Date <= request.ToDate.Value);

			filter = filter.AndAlso(s => !s.IsDeleted); 

			var result = await _classSessionRepo.GetPagedAsync(
				filter: filter,
				include: q => q.Include(c => c.Class).Include(c => c.Subject).Include(c => c.Teacher),
				orderBy: q => q.OrderByDescending(c => c.Date).ThenBy(c => c.PeriodNumber),
				pageNumber: request.PageNumber,
				pageSize: request.PageSize,
				asNoTracking: true
			);

			var dtoList = _mapper.Map<List<ClassSessionDto>>(result.Items);
			if (dtoList == null || !dtoList.Any())
				return PagedResponse<ClassSessionDto>.Fail("No class sessions found", request.PageNumber, request.PageSize);

			return PagedResponse<ClassSessionDto>.Ok(dtoList, result.TotalCount, request.PageNumber, request.PageSize);
		}

		public async Task<BaseResponse<byte[]>> ExportClassTimetableToExcelAsync(Guid classId, DateTime from, DateTime to)
		{
			try
			{
				if (classId == Guid.Empty)
					return BaseResponse<byte[]>.Fail("Invalid class ID");

				if (from == default || to == default || from > to)
					return BaseResponse<byte[]>.Fail("Invalid date range");

				var sessions = await _classSessionRepo.GetAllAsync(
					filter: cs => cs.ClassId == classId && cs.Date >= from && cs.Date <= to && !cs.IsDeleted,
					include: q => q.Include(cs => cs.Subject).Include(cs => cs.Teacher).Include(cs => cs.Class),
					asNoTracking: true
				);

				if (!sessions.Any())
					return BaseResponse<byte[]>.Fail("No sessions found for this class");

				ExcelPackage.License.SetNonCommercialOrganization("EduConnect");
				using var package = new ExcelPackage();
				var worksheet = package.Workbook.Worksheets.Add("Class Timetable");

				// Headers
				worksheet.Cells[1, 1].Value = "Date";
				worksheet.Cells[1, 2].Value = "Day"; 
				worksheet.Cells[1, 3].Value = "Period";
				worksheet.Cells[1, 4].Value = "Class";
				worksheet.Cells[1, 5].Value = "Subject";
				worksheet.Cells[1, 6].Value = "Teacher";
				worksheet.Cells[1, 7].Value = "Lesson Content";

				int row = 2;
				foreach (var session in sessions.OrderBy(s => s.Date).ThenBy(s => s.PeriodNumber))
				{
					var dayOfWeek = session.Date.ToString("dddd", new CultureInfo("vi-VN"));

					worksheet.Cells[row, 1].Value = session.Date.ToString("yyyy-MM-dd");
					worksheet.Cells[row, 2].Value = dayOfWeek;
					worksheet.Cells[row, 3].Value = session.PeriodNumber;
					worksheet.Cells[row, 4].Value = session.Class?.ClassName;
					worksheet.Cells[row, 5].Value = session.Subject?.SubjectName;
					worksheet.Cells[row, 6].Value = session.Teacher?.UserName;
					worksheet.Cells[row, 7].Value = session.LessonContent;
					row++;
				}

				worksheet.Cells.AutoFitColumns();

				return BaseResponse<byte[]>.Ok(package.GetAsByteArray(), "Class timetable exported successfully");
			}
			catch (Exception ex)
			{
				return BaseResponse<byte[]>.Fail("An error occurred while exporting class timetable: " + ex.Message);
			}
		}

		public async Task<BaseResponse<byte[]>> ExportTeacherTimetableToExcelAsync(Guid teacherId, DateTime from, DateTime to)
		{
			try
			{
				if (teacherId == Guid.Empty)
					return BaseResponse<byte[]>.Fail("Invalid teacher ID");

				if (from == default || to == default || from > to)
					return BaseResponse<byte[]>.Fail("Invalid date range");

				var sessions = await _classSessionRepo.GetAllAsync(
					filter: cs => cs.TeacherId == teacherId && cs.Date >= from && cs.Date <= to && !cs.IsDeleted,
					include: q => q.Include(cs => cs.Subject).Include(cs => cs.Class).Include(cs => cs.Teacher),
					asNoTracking: true
				);

				if (!sessions.Any())
					return BaseResponse<byte[]>.Fail("No sessions found for this teacher");

				ExcelPackage.License.SetNonCommercialOrganization("EduConnect");
				using var package = new ExcelPackage();
				var worksheet = package.Workbook.Worksheets.Add("Teacher Timetable");

				// Headers
				worksheet.Cells[1, 1].Value = "Date";
				worksheet.Cells[1, 2].Value = "Day";
				worksheet.Cells[1, 3].Value = "Period";
				worksheet.Cells[1, 4].Value = "Class";
				worksheet.Cells[1, 5].Value = "Subject";
				worksheet.Cells[1, 6].Value = "Lesson Content";

				int row = 2;
				foreach (var session in sessions.OrderBy(s => s.Date).ThenBy(s => s.PeriodNumber))
				{
					var dayOfWeek = session.Date.ToString("dddd", new CultureInfo("vi-VN"));

					worksheet.Cells[row, 1].Value = session.Date.ToString("yyyy-MM-dd");
					worksheet.Cells[row, 2].Value = dayOfWeek;
					worksheet.Cells[row, 3].Value = session.PeriodNumber;
					worksheet.Cells[row, 4].Value = session.Class?.ClassName;
					worksheet.Cells[row, 5].Value = session.Subject?.SubjectName;
					worksheet.Cells[row, 6].Value = session.LessonContent;
					row++;
				}

				worksheet.Cells.AutoFitColumns();

				return BaseResponse<byte[]>.Ok(package.GetAsByteArray(), "Teacher timetable exported successfully");
			}
			catch (Exception ex)
			{
				return BaseResponse<byte[]>.Fail("An error occurred while exporting teacher timetable: " + ex.Message);
			}
		}

		public async Task<BaseResponse<string>> CreateClassSessionAsync(CreateClassSessionRequest request)
		{
			var validationResult = await _validatorCreate.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
				return BaseResponse<string>.Fail(string.Join(" | ", errors));
			}

			// Optional: prevent duplicates
			var isDuplicate = await _classSessionRepo.AnyAsync(cs =>
				cs.ClassId == request.ClassId &&
				cs.Date == request.Date &&
				cs.PeriodNumber == request.PeriodNumber);
			if (isDuplicate)
				return BaseResponse<string>.Fail("A session already exists at this date and period for this class.");

			var entity = _mapper.Map<ClassSession>(request);
			await _classSessionRepo.AddAsync(entity);
			var saved = await _classSessionRepo.SaveChangesAsync();

			return saved 
				? BaseResponse<string>.Ok("Class session created successfully") 
				: BaseResponse<string>.Fail("Fail to create Class session");
		}

		public async Task<BaseResponse<string>> UpdateClassSessionAsync(UpdateClassSessionRequest request, Guid currentTeacherId, Guid classSessionId)
		{
			var validation = await _validatorUpdate.ValidateAsync(request);
			if (!validation.IsValid)
				return BaseResponse<string>.Fail(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));

			var session = await _classSessionRepo.GetByIdAsync(
				cs => cs.ClassSessionId == classSessionId && cs.TeacherId == currentTeacherId && !cs.IsDeleted);

			if (session == null)
				return BaseResponse<string>.Fail("Class session not found or not authorized or deleted");

			session.LessonContent = request.LessonContent;
			session.TotalAbsentStudents = request.TotalAbsentStudents;
			session.GeneralBehaviorNote = request.GeneralBehaviorNote;

			_classSessionRepo.Update(session);

			bool saved = await _classSessionRepo.SaveChangesAsync();
			return saved
				? BaseResponse<string>.Ok("Class session updated successfully")
				: BaseResponse<string>.Fail("Failed to update class session");
		}

		public async Task<BaseResponse<string>> UpdateClassSessionByAdminAsync(UpdateClassSessionByAdminRequest request, Guid classSessionId)
		{
			var validation = await _validatorUpdateByAdmin.ValidateAsync(request);
			if (!validation.IsValid)
				return BaseResponse<string>.Fail(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));

			var session = await _classSessionRepo.GetByIdAsync(s => s.ClassSessionId == classSessionId);
			if (session == null)
				return BaseResponse<string>.Fail("Class session not found");

			_mapper.Map(request, session);

			_classSessionRepo.Update(session);
			var saved = await _classSessionRepo.SaveChangesAsync();
			return saved
				? BaseResponse<string>.Ok("Class session updated successfully")
				: BaseResponse<string>.Fail("Failed to update class session");
		}

		public async Task<BaseResponse<string>> SoftDeleteClassSessionAsync(Guid id)
		{
			var session = await _classSessionRepo.GetByIdAsync(s => s.ClassSessionId == id && !s.IsDeleted);
			if (session == null)
				return BaseResponse<string>.Fail("Class session not found or deleted");

			if (session.Date < DateTime.UtcNow)
				return BaseResponse<string>.Fail("Cannot delete past sessions");

			session.IsDeleted = true;
			session.DeleteAt = DateTime.UtcNow;

			_classSessionRepo.Update(session);
			var saved = await _classSessionRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class session marked as deleted")
				: BaseResponse<string>.Fail("Failed to delete class session");
		}

		public async Task<BaseResponse<string>> DeleteClassSessionAsync(Guid id)
		{
			var session = await _classSessionRepo.GetByIdAsync(
				s => s.ClassSessionId == id,
				include: q => q
					.Include(c => c.StudentBehaviorNotes)
					.Include(c => c.ClassBehaviorLogs)
			);

			if (session == null)
				return BaseResponse<string>.Fail("Class session not found");

			if (session.Date < DateTime.UtcNow)
				return BaseResponse<string>.Fail("Cannot delete past sessions");

			if (session.ClassBehaviorLogs.Count != 0)
				return BaseResponse<string>.Fail("Cannot delete session with class behavior logs");

			if (session.StudentBehaviorNotes.Count != 0)
				return BaseResponse<string>.Fail("Cannot delete session with student behavior notes");

			// ClassBehaviorLogs will be auto-deleted by cascade
			_classSessionRepo.Remove(session);
			var saved = await _classSessionRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class session deleted")
				: BaseResponse<string>.Fail("Failed to delete class session");
		}
	}
}
