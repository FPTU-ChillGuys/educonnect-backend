using AutoMapper;
using EduConnect.Application.DTOs.Responses.NotificationResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Mappings
{
	public class NotificationProfile : Profile
	{
		public NotificationProfile()
		{
			CreateMap<Notification, NotificationDto>();
		}
	}
}
