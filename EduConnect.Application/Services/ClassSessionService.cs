﻿using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
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
		private readonly IValidator<TimetableRequest> _timetableValidator;
		private readonly IValidator<CreateClassSessionRequest> _validatorCreate;
		private readonly IValidator<UpdateClassSessionRequest> _validatorUpdate;
		private readonly IValidator<UpdateClassSessionByAdminRequest> _validatorUpdateByAdmin;
		private readonly IMapper _mapper;
		public ClassSessionService(IGenericRepository<ClassSession> classSessionRepo,
			IValidator<CreateClassSessionRequest> validatorCreate,
			IValidator<UpdateClassSessionRequest> validatorUpdate,
			IValidator<UpdateClassSessionByAdminRequest> validatorUpdateByAdmin,
			IValidator<TimetableRequest> timetableValidator,
			IMapper mapper)
		{
			_classSessionRepo = classSessionRepo;
			_validatorCreate = validatorCreate;
			_validatorUpdate = validatorUpdate;
			_validatorUpdateByAdmin = validatorUpdateByAdmin;
			_mapper = mapper;
			_timetableValidator = timetableValidator;
		}

		public async Task<BaseResponse<List<TimetableViewDto>>> GetTimetableAsync(TimetableRequest request)
		{
			var validationResult = await _timetableValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return BaseResponse<List<TimetableViewDto>>.Fail(errors);
			}

			try
			{
				Expression<Func<ClassSession, bool>> filter = cs => !cs.IsDeleted;

				if (request.Mode == "Class")
					filter = filter.AndAlso(cs => cs.ClassId == request.TargetId);
				else
					filter = filter.AndAlso(cs => cs.TeacherId == request.TargetId);

				DateTime? from = request.From != default ? Utils.FixWronglyParsedDate(request.From) : null;
				DateTime? to = request.To != default ? Utils.FixWronglyParsedDate(request.To) : null;

				if (from.HasValue && to.HasValue)
					filter = filter.AndAlso(cs => cs.Date >= from.Value && cs.Date <= to.Value);

				var sessions = await _classSessionRepo.GetAllAsync(
					filter: filter,
					include: q => q
						.Include(cs => cs.Subject)
						.Include(cs => cs.Class)
						.Include(cs => cs.Teacher)
						.Include(cs => cs.Period),
					asNoTracking: true
				);

				if (!sessions.Any())
					return BaseResponse<List<TimetableViewDto>>.Fail("No timetable sessions found");

				var grouped = sessions
					.GroupBy(cs => cs.Date.Date)
					.OrderBy(g => g.Key)
					.Select(g => new TimetableViewDto
					{
						Date = g.Key,
						DayOfWeek = g.Key.ToString("dddd", new CultureInfo("vi-VN")),
						Periods = g.OrderBy(cs => cs.Period.PeriodNumber)
							.Select(cs => new PeriodSlotDto
							{
								PeriodNumber = cs.Period.PeriodNumber,
								ClassName = cs.Class?.ClassName ?? "N/A",
								SubjectName = cs.Subject?.SubjectName ?? "N/A",
								TeacherName = cs.Teacher?.FullName ?? "N/A",
								LessonContent = cs.LessonContent
							}).ToList()
					}).ToList();

				var label = request.Mode == "Class" ? "Class" : "Teacher";

				return BaseResponse<List<TimetableViewDto>>.Ok(grouped, $"{label} timetable loaded successfully");
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

			DateTime? from = request.FromDate.HasValue ? Utils.FixWronglyParsedDate(request.FromDate.Value) : null;
			DateTime? to = request.ToDate.HasValue ? Utils.FixWronglyParsedDate(request.ToDate.Value) : null;

			if (from.HasValue)
				filter = filter.AndAlso(s => s.Date >= from.Value);

			if (to.HasValue)
				filter = filter.AndAlso(s => s.Date <= to.Value);

			filter = filter.AndAlso(s => !s.IsDeleted); 

			var (items, total) = await _classSessionRepo.GetPagedAsync(
				filter: filter,
				include: q => q.Include(c => c.Class).Include(c => c.Subject).Include(c => c.Period).Include(c => c.Teacher),
				orderBy: q => q.OrderByDescending(c => c.Date).ThenBy(c => c.Period.PeriodNumber),
				pageNumber: request.PageNumber,
				pageSize: request.PageSize,
				asNoTracking: true
			);

			var dtoList = _mapper.Map<List<ClassSessionDto>>(items);
			if (dtoList == null || dtoList.Count == 0)
				return PagedResponse<ClassSessionDto>.Fail("No class sessions found", request.PageNumber, request.PageSize);

			return PagedResponse<ClassSessionDto>.Ok(dtoList, total, request.PageNumber, request.PageSize);
		}

		public async Task<BaseResponse<byte[]>> ExportTimetableToExcelAsync(TimetableRequest request)
		{
			try
			{
				if (request.TargetId == Guid.Empty)
					return BaseResponse<byte[]>.Fail("Invalid target ID");

				DateTime? from = request.From != default ? Utils.FixWronglyParsedDate(request.From) : null;
				DateTime? to = request.To != default ? Utils.FixWronglyParsedDate(request.To) : null;

				if (!from.HasValue || !to.HasValue || from > to)
					return BaseResponse<byte[]>.Fail("Invalid date range");

				Expression<Func<ClassSession, bool>> filter = cs => !cs.IsDeleted;

				if (request.Mode == "Class")
					filter = filter.AndAlso(cs => cs.ClassId == request.TargetId);
				else if (request.Mode == "Teacher")
					filter = filter.AndAlso(cs => cs.TeacherId == request.TargetId);
				else
					return BaseResponse<byte[]>.Fail("Invalid mode. Expected 'Class' or 'Teacher'");

				filter = filter.AndAlso(cs => cs.Date >= from.Value && cs.Date <= to.Value);

				var sessions = await _classSessionRepo.GetAllAsync(
					filter: filter,
					include: q => q.Include(cs => cs.Subject)
						.Include(cs => cs.Teacher)
						.Include(cs => cs.Period)
						.Include(cs => cs.Class),
					asNoTracking: true
				);

				if (!sessions.Any())
					return BaseResponse<byte[]>.Fail("No sessions found for export");

				ExcelPackage.License.SetNonCommercialOrganization("EduConnect");
				using var package = new ExcelPackage();
				var worksheetName = request.Mode == "Class" ? "Class Timetable" : "Teacher Timetable";
				var worksheet = package.Workbook.Worksheets.Add(worksheetName);

				// Set headers
				worksheet.Cells[1, 1].Value = "Date";
				worksheet.Cells[1, 2].Value = "Day";
				worksheet.Cells[1, 3].Value = "Period";
				worksheet.Cells[1, 4].Value = "Class";
				worksheet.Cells[1, 5].Value = "Subject";

				int row = 2;
				if (request.Mode == "Class")
				{
					worksheet.Cells[1, 6].Value = "Teacher";
					worksheet.Cells[1, 7].Value = "Lesson Content";
					foreach (var session in sessions.OrderBy(s => s.Date).ThenBy(s => s.Period.PeriodNumber))
					{
						worksheet.Cells[row, 1].Value = session.Date.ToString("yyyy-MM-dd");
						worksheet.Cells[row, 2].Value = session.Date.ToString("dddd", new CultureInfo("vi-VN"));
						worksheet.Cells[row, 3].Value = session.Period.PeriodNumber;
						worksheet.Cells[row, 4].Value = session.Class?.ClassName;
						worksheet.Cells[row, 5].Value = session.Subject?.SubjectName;
						worksheet.Cells[row, 6].Value = session.Teacher?.UserName;
						worksheet.Cells[row, 7].Value = session.LessonContent;
						row++;
					}
				}
				else // Teacher
				{
					worksheet.Cells[1, 6].Value = "Lesson Content";
					foreach (var session in sessions.OrderBy(s => s.Date).ThenBy(s => s.Period.PeriodNumber))
					{
						worksheet.Cells[row, 1].Value = session.Date.ToString("yyyy-MM-dd");
						worksheet.Cells[row, 2].Value = session.Date.ToString("dddd", new CultureInfo("vi-VN"));
						worksheet.Cells[row, 3].Value = session.Period.PeriodNumber;
						worksheet.Cells[row, 4].Value = session.Class?.ClassName;
						worksheet.Cells[row, 5].Value = session.Subject?.SubjectName;
						worksheet.Cells[row, 6].Value = session.LessonContent;
						row++;
					}
				}

				worksheet.Cells.AutoFitColumns();

				return BaseResponse<byte[]>.Ok(package.GetAsByteArray(), "Timetable exported successfully");
			}
			catch (Exception ex)
			{
				return BaseResponse<byte[]>.Fail("An error occurred during export: " + ex.Message);
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

			var isDuplicate = await _classSessionRepo.AnyAsync(cs =>
				cs.ClassId == request.ClassId &&
				cs.Date == request.Date &&
				cs.PeriodId == request.PeriodId); 
			if (isDuplicate)
				return BaseResponse<string>.Fail("A session already exists at this date and period for this class.");

			var entity = _mapper.Map<ClassSession>(request);
			await _classSessionRepo.AddAsync(entity);
			var saved = await _classSessionRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class session created successfully")
				: BaseResponse<string>.Fail("Failed to create class session");
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

			var isDuplicate = await _classSessionRepo.AnyAsync(cs =>
				cs.ClassId == request.ClassId &&
				cs.Date == request.Date &&
				cs.PeriodId == request.PeriodId);
			if (isDuplicate)
				return BaseResponse<string>.Fail("A session already exists at this date and period for this class.");

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
