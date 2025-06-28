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
			CreateMap<CreateClassRequest, Class>()
			.ForMember(dest => dest.ClassId, opt => opt.MapFrom(_ => Guid.NewGuid()));

			CreateMap<UpdateClassRequest, Class>();

			CreateMap<Class, ClassDto>()
			.ForMember(dest => dest.HomeroomTeacherName, opt => opt.MapFrom(src => src.HomeroomTeacher.UserName));
		}
	}
}
