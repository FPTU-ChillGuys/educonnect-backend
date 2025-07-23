using AutoMapper;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.DTOs.Responses.ReportResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Mappings
{
	public class ReportProfile : Profile
	{
		public ReportProfile()
		{
			var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			CreateMap<CreateClassReportRequest, ClassReport>()
				.ForMember(dest => dest.GeneratedDate, opt => opt.MapFrom(_ =>
					TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)));

			CreateMap<ClassReport, ClassReportDto>();

			CreateMap<CreateStudentReportRequest, StudentReport>()
				.ForMember(dest => dest.GeneratedDate, opt => opt.MapFrom(_ =>
					TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone)));

			CreateMap<StudentReport, StudentReportDto>();
		}
	}
}
