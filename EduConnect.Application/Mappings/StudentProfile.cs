using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.DTOs.Responses.StudentResponses;
using EduConnect.Domain.Entities;
using AutoMapper;

namespace EduConnect.Application.Mappings
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
			CreateMap<Student, StudentDto>()
				.ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
				.ForMember(dest => dest.ParentEmail, opt => opt.MapFrom(src => src.Parent.Email))
				.ForMember(dest => dest.ParentFullName, opt => opt.MapFrom(src => src.Parent.FullName))
				.ForMember(dest => dest.ParentPhoneNumber, opt => opt.MapFrom(src => src.Parent.PhoneNumber));

			CreateMap<CreateStudentRequest, Student>();

			CreateMap<UpdateStudentRequest, Student>()
				.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
		}
	}
}
