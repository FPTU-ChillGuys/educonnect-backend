using EduConnect.Application.Commons;
using EduConnect.Application.DTOs.Requests.ClassRequests;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IClassService
	{
		Task<BaseResponse<int>> CountClassesAsync();
		Task<BaseResponse<string>> CreateClassAsync(CreateClassRequest request);
	}
}
