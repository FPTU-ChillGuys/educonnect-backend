using AutoMapper;
using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Mappings
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dest => dest.IsHomeroomTeacher, opt => opt.MapFrom(src => src.HomeroomClasses != null && src.HomeroomClasses.Any()))
				.ForMember(dest => dest.IsSubjectTeacher, opt => opt.MapFrom(src => src.TeachingSessions != null && src.TeachingSessions.Any()));
		}
	}
}
