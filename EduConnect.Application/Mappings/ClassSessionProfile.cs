using AutoMapper;
using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.DTOs.Responses.ClassSessionResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Mappings
{
	public class ClassSessionProfile : Profile
	{
		public ClassSessionProfile()
		{
			CreateMap<CreateClassSessionRequest, ClassSession>()
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

			CreateMap<UpdateClassSessionByAdminRequest, ClassSession>();

			CreateMap<ClassSession, ClassSessionDto>()
				.ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
				.ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
				.ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.FullName))
				.ForMember(dest => dest.PeriodNumber, opt => opt.MapFrom(src => src.Period.PeriodNumber));
		}
	}
}
