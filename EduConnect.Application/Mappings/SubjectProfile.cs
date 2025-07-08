using AutoMapper;
using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.DTOs.Responses.SubjectResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Mappings
{
	public class SubjectProfile : Profile
	{
		public SubjectProfile()
		{
			CreateMap<Subject, SubjectDto>();
			CreateMap<CreateSubjectRequest, Subject>();
			CreateMap<UpdateSubjectRequest, Subject>();
		}
	}
}
