using EduConnect.Application.DTOs.Responses.ClassResponses;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Extensions;
using EduConnect.Application.Commons.Dtos;
using Microsoft.EntityFrameworkCore;
using EduConnect.Domain.Entities;
using System.Linq.Expressions;
using FluentValidation;
using AutoMapper;

namespace EduConnect.Application.Services
{
	public class ClassService : IClassService
	{
		private readonly IValidator<CreateClassRequest> _createValidator;
		private readonly IValidator<UpdateClassRequest> _updateValidator;
		private readonly IValidator<ClassPagingRequest> _pagingValidator;
		private readonly IGenericRepository<Class> _classRepo;
		private readonly IMapper _mapper;

		public ClassService(IValidator<CreateClassRequest> createValidator,
			IValidator<UpdateClassRequest> updateValidator,
			IValidator<ClassPagingRequest> pagingValidator,
			IGenericRepository<Class> classRepo,
			IMapper mapper)
		{
			_createValidator = createValidator;
			_updateValidator = updateValidator;
			_pagingValidator = pagingValidator;
			_classRepo = classRepo;
			_mapper = mapper;
		}

		public async Task<BaseResponse<int>> CountClassesAsync()
		{
			return await _classRepo.CountAsync()
				.ContinueWith(task => task.IsCompletedSuccessfully
					? BaseResponse<int>.Ok(task.Result)
					: BaseResponse<int>.Fail("Failed to retrieve class count"));
		}

		public async Task<PagedResponse<ClassDto>> GetPagedClassesAsync(ClassPagingRequest request)
		{
			var validationResult = await _pagingValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return PagedResponse<ClassDto>.Fail(errors, request.PageNumber, request.PageSize);
			}

			Expression<Func<Class, bool>> filter = c => true;

			if (!string.IsNullOrWhiteSpace(request.Keyword))
			{
				filter = filter.AndAlso(c =>
					c.ClassName.Contains(request.Keyword) ||
					c.GradeLevel.Contains(request.Keyword) ||
					c.AcademicYear.Contains(request.Keyword));
			}

			var (classes, totalCount) = await _classRepo.GetPagedAsync(
				filter: filter,
				include: q => q.Include(c => c.HomeroomTeacher),
				orderBy: q => q.ApplySorting(request.SortBy, request.SortDescending),
				pageNumber: request.PageNumber,
				pageSize: request.PageSize,
				asNoTracking: true
			);

			var dtoList = _mapper.Map<List<ClassDto>>(classes);

			return PagedResponse<ClassDto>.Ok(dtoList, totalCount, request.PageNumber, request.PageSize);
		}


		public async Task<BaseResponse<string>> CreateClassAsync(CreateClassRequest request)
		{
			var validationResult = await _createValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return BaseResponse<string>.Fail(errors);
			}

			var newClass = _mapper.Map<Class>(request);

			await _classRepo.AddAsync(newClass);
			var saved = await _classRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class created successfully")
				: BaseResponse<string>.Fail("Failed to create class");
		}

		public async Task<BaseResponse<string>> UpdateClassAsync(Guid id, UpdateClassRequest request)
		{
			var validationResult = await _updateValidator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return BaseResponse<string>.Fail(errors);
			}

			var existingClass = await _classRepo.GetByIdAsync(c => c.ClassId == id);
			if (existingClass == null)
				return BaseResponse<string>.Fail("Class not found");

			_mapper.Map(request, existingClass); 

			_classRepo.Update(existingClass);
			var saved = await _classRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class updated successfully")
				: BaseResponse<string>.Fail("Failed to update class");
		}

		public async Task<BaseResponse<string>> DeleteClassAsync(Guid classId)
		{
			var entity = await _classRepo.GetByIdAsync(c => c.ClassId == classId);
			if (entity == null)
				return BaseResponse<string>.Fail("Class not found");

			_classRepo.Remove(entity);
			var saved = await _classRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Class deleted successfully")
				: BaseResponse<string>.Fail("Failed to delete class");
		}

		public async Task<BaseResponse<ClassDto>> GetClassByIdAsync(Guid classId)
		{
			var classEntity = await _classRepo.GetByIdAsync(
				c => c.ClassId == classId,
				include: q => q.Include(c => c.HomeroomTeacher),
				asNoTracking: true
			);

			if (classEntity is null)
				return BaseResponse<ClassDto>.Fail("Class not found");

			var dto = _mapper.Map<ClassDto>(classEntity);
			return BaseResponse<ClassDto>.Ok(dto);
		}

		public async Task<BaseResponse<List<ClassDto>>> GetClassesByTeacherIdAsync(Guid teacherId)
		{
			var classList = await _classRepo.GetAllAsync(
				filter: c => c.HomeroomTeacherId == teacherId,
				include: q => q.Include(c => c.HomeroomTeacher),
				asNoTracking: true
			);

			var dtoList = _mapper.Map<List<ClassDto>>(classList);
			return BaseResponse<List<ClassDto>>.Ok(dtoList);
		}

		public async Task<BaseResponse<List<ClassDto>>> GetClassesByStudentIdAsync(Guid studentId)
		{
			var classList = await _classRepo.GetAllAsync(
				filter: c => c.Students.Any(s => s.StudentId == studentId),
				include: q => q.Include(c => c.Students).Include(c => c.HomeroomTeacher),
				orderBy: q => q.OrderByDescending(c => c.AcademicYear).ThenBy(c => c.ClassName),
				asNoTracking: true
			);

			var dtoList = _mapper.Map<List<ClassDto>>(classList);
			return BaseResponse<List<ClassDto>>.Ok(dtoList);
		}
	}
}
