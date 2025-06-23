using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons;
using EduConnect.Domain.Entities;
using FluentValidation;
using AutoMapper;

namespace EduConnect.Application.Services
{
	public class ClassService : IClassService
	{
		private readonly IValidator<CreateClassRequest> _validator;	
		private readonly IGenericRepository<Class> _classRepo;
		private readonly IMapper _mapper;

		public ClassService( IValidator<CreateClassRequest> validator,
			IGenericRepository<Class> classRepo,
			IMapper mapper)
		{
			_validator = validator;
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

		public async Task<BaseResponse<string>> CreateClassAsync(CreateClassRequest request)
		{
			var validationResult = await _validator.ValidateAsync(request);
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
	}
}
