using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Responses.NotificationResponses;
using EduConnect.Application.DTOs.Requests.NotificationRequests;

namespace EduConnect.Application.Interfaces.Services
{
	public interface INotificationService
	{
		Task<BaseResponse<List<NotificationDto>>> GetNotificationsByUserIdAsync(Guid userId);
		Task<BaseResponse<NotificationDto>> CreateNotificationAsync(CreateNotificationRequest request);
	}
}
