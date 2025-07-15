using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Responses.PeriodResponses;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IPeriodService
	{
		Task<BaseResponse<List<PeriodLookuptDto>>> GetPeriodLookupAsync();
	}
}
