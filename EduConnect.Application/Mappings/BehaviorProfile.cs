using AutoMapper;
using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.DTOs.Responses.BehaviorResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Mappings
{
	public class BehaviorProfile : Profile
	{
		public BehaviorProfile()
		{
			var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			CreateMap<ClassBehaviorLog, ClassBehaviorLogDto>();
			CreateMap<StudentBehaviorNote, StudentBehaviorNoteDto>()
				.ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName));

			CreateMap<CreateClassBehaviorLogRequest, ClassBehaviorLog>()
				.ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ =>
					TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)));

			CreateMap<CreateStudentBehaviorNoteRequest, StudentBehaviorNote>()
				.ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ =>
					TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)));
		}
	}
}
