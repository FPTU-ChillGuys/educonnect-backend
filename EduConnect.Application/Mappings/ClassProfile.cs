using EduConnect.Application.DTOs.Responses.ClassResponses;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Domain.Entities;
using AutoMapper;

namespace EduConnect.Application.Mappings
{
	public class ClassProfile : Profile
	{
		public ClassProfile()
		{
			CreateMap<CreateClassRequest, Class>();

			CreateMap<Class, ClassDto>()
				.ForMember(dest => dest.HomeroomTeacherEmail,
					opt => opt.MapFrom(src => src.HomeroomTeacher.Email));
		}
	}
}
