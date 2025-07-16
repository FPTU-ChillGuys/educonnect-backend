using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Responses.NotificationResponses;

namespace EduConnect.Application.Interfaces.Services
{
	public interface INotificationService
	{
		Task<BaseResponse<List<NotificationDto>>> GetNotificationsByUserIdAsync(Guid userId);
	}
}
