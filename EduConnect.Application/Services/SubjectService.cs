using EduConnect.Application.Commons.Extensions;
using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.DTOs.Responses.SubjectResponses;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Domain.Entities;
using System.Linq.Expressions;
using FluentValidation;
using AutoMapper;

namespace EduConnect.Application.Services
{
	public class SubjectService : ISubjectService
	{
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Subject> _subjectRepo;
		private readonly IValidator<CreateSubjectRequest> _createValidator;
		private readonly IValidator<UpdateSubjectRequest> _updateValidator;

		public SubjectService(
			IGenericRepository<Subject> subjectRepo,
			IMapper mapper,
			IValidator<CreateSubjectRequest> createValidator,
			IValidator<UpdateSubjectRequest> updateValidator)
		{
			_subjectRepo = subjectRepo;
			_mapper = mapper;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
		}

		public async Task<PagedResponse<SubjectDto>> GetPagedSubjectsAsync(SubjectPagingRequest request)
		{
			// Filter with optional keyword
			Expression<Func<Subject, bool>> filter = s => string.IsNullOrEmpty(request.Keyword)
				|| s.SubjectName.Contains(request.Keyword);

			// Get paged and sorted results
			var (items, totalCount) = await _subjectRepo.GetPagedAsync(
				filter: filter,
				orderBy: q => q.ApplySorting(request.SortBy, request.SortDescending),
				pageNumber: request.PageNumber,
				pageSize: request.PageSize,
				asNoTracking: true
			);

			var dtos = _mapper.Map<List<SubjectDto>>(items);

			return PagedResponse<SubjectDto>.Ok(dtos, totalCount, request.PageNumber, request.PageSize);
		}


		public async Task<BaseResponse<SubjectDto>> GetSubjectByIdAsync(Guid id)
		{
			var subject = await _subjectRepo.GetByIdAsync(s => s.SubjectId == id, asNoTracking: true);
			if (subject == null)
				return BaseResponse<SubjectDto>.Fail("Subject not found");

			return BaseResponse<SubjectDto>.Ok(_mapper.Map<SubjectDto>(subject));
		}

		public async Task<BaseResponse<string>> CreateSubjectAsync(CreateSubjectRequest request)
		{
			var validation = await _createValidator.ValidateAsync(request);
			if (!validation.IsValid)
				return BaseResponse<string>.Fail(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));

			var subject = _mapper.Map<Subject>(request);
			await _subjectRepo.AddAsync(subject);
			var saved = await _subjectRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Subject created successfully")
				: BaseResponse<string>.Fail("Failed to create subject");
		}

		public async Task<BaseResponse<string>> UpdateSubjectAsync(Guid id, UpdateSubjectRequest request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (!validation.IsValid)
				return BaseResponse<string>.Fail(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));

			var existing = await _subjectRepo.GetByIdAsync(s => s.SubjectId == id);
			if (existing == null)
				return BaseResponse<string>.Fail("Subject not found");

			_mapper.Map(request, existing);
			_subjectRepo.Update(existing);

			var saved = await _subjectRepo.SaveChangesAsync();
			return saved
				? BaseResponse<string>.Ok("Subject updated successfully")
				: BaseResponse<string>.Fail("Failed to update subject");
		}

		public async Task<BaseResponse<string>> DeleteSubjectAsync(Guid id)
		{
			var subject = await _subjectRepo.GetByIdAsync(s => s.SubjectId == id);
			if (subject == null)
				return BaseResponse<string>.Fail("Subject not found");

			_subjectRepo.Remove(subject);
			var saved = await _subjectRepo.SaveChangesAsync();

			return saved
				? BaseResponse<string>.Ok("Subject deleted successfully")
				: BaseResponse<string>.Fail("Failed to delete subject");
		}
	}
}
