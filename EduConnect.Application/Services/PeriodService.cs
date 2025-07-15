using EduConnect.Application.DTOs.Responses.PeriodResponses;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Services
{
	public class PeriodService : IPeriodService
	{
		private readonly IGenericRepository<Period> _periodRepository;

		public PeriodService(IGenericRepository<Period> periodRepository)
		{
			_periodRepository = periodRepository;
		}

		public async Task<BaseResponse<List<PeriodLookuptDto>>> GetPeriodLookupAsync()
		{
			var periods = await _periodRepository.GetAllAsync(
				orderBy: q => q.OrderBy(p => p.PeriodNumber),
				asNoTracking: true
			);

			var dtoList = periods.Select(p => new PeriodLookuptDto
			{
				PeriodId = p.PeriodId,
				PeriodNumber = p.PeriodNumber,
				StartTime = p.StartTime,
				EndTime = p.EndTime
			}).ToList();

			if (!dtoList.Any())
				return BaseResponse<List<PeriodLookuptDto>>.Fail("No periods found");

			return BaseResponse<List<PeriodLookuptDto>>.Ok(dtoList, "Periods retrieved successfully");
		}
	}
}
