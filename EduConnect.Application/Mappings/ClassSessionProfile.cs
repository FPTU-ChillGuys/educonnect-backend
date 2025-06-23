using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Domain.Entities;
using AutoMapper;

namespace EduConnect.Application.Mappings
{
	public class ClassSessionProfile : Profile
	{
		public ClassSessionProfile()
		{
			CreateMap<CreateClassSessionRequest, ClassSession>()
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

			CreateMap<UpdateClassSessionByAdminRequest, ClassSession>();
		}
	}
}
