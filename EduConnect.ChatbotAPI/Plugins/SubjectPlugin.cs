using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.DTOs.Responses.SubjectResponses;
using EduConnect.Application.Interfaces.Services;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace EduConnect.ChatbotAPI.Plugins
{
    public class SubjectPlugin (
            ISubjectService subjectService
        )
    {
        [KernelFunction("GetPagedSubjects")]
        [Description("Retrieves a paginated list of subjects.")]
        public async Task<List<SubjectDto>> GetSubjects()
        {
            
            var subjects = await subjectService.GetPagedSubjectsAsync(
                new SubjectPagingRequest
                {
                    PageSize = 100
                }
            );
            return subjects!.Data!;

        }

        [KernelFunction("GetSubjectById")]
        [Description("Retrieves detailed information about a subject using its ID.")]
        public async Task<List<SubjectDto>> GetSubjectByName(string subjectName)
        {
            var subjects = await subjectService.GetPagedSubjectsAsync(
                new SubjectPagingRequest
                {
                    PageSize = 100,
                    Keyword = subjectName
                }
            );
            if (subjects.Data == null || subjects.Data.Count == 0)
            {
                return new List<SubjectDto>();
            }
            return subjects.Data.Where(s => s.SubjectName.Contains(subjectName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
