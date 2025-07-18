﻿using EduConnect.Application.DTOs.Responses.ClassSessionResponses;
using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IClassSessionService
	{
		Task<PagedResponse<ClassSessionDto>> GetPagedClassSessionsAsync(ClassSessionPagingRequest request);
		Task<BaseResponse<List<TimetableViewDto>>> GetTimetableAsync(TimetableRequest request);
		Task<BaseResponse<byte[]>> ExportTimetableToExcelAsync(TimetableRequest request);
		Task<BaseResponse<string>> CreateClassSessionAsync(CreateClassSessionRequest request);
		Task<BaseResponse<string>> UpdateClassSessionAsync(UpdateClassSessionRequest request, Guid currentTeacherId, Guid classSessionId);
		Task<BaseResponse<string>> UpdateClassSessionByAdminAsync(UpdateClassSessionByAdminRequest request, Guid classSessionId);
		Task<BaseResponse<string>> SoftDeleteClassSessionAsync(Guid id);
		Task<BaseResponse<string>> DeleteClassSessionAsync(Guid id);
	}
}
