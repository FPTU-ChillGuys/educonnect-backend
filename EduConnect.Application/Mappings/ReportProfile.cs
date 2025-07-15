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
			CreateMap<CreateClassReportRequest, ClassReport>()
				.ForMember(dest => dest.GeneratedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

			CreateMap<ClassReport, ClassReportDto>();

			CreateMap<CreateStudentReportRequest, StudentReport>()
				.ForMember(dest => dest.GeneratedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

			CreateMap<StudentReport, StudentReportDto>();
		}
	}
}
