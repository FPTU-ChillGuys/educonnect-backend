using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infrastructure.Repositories
{
	public class StudentReportRepository : GenericRepository<StudentReport>, IStudentReportRepository
	{
		public StudentReportRepository(EduConnectDbContext context) : base(context) { }

		public async Task<StudentReport?> GetLatestStudentReportForNotificationAsync(GetStudentReportToNotifyRequest request)
		{
			IQueryable<StudentReport> query = _context.StudentReports
				.Where(r => r.StudentId == request.StudentId);

			if (request.Type.HasValue)
				query = query.Where(r => r.Type == request.Type.Value);

			if (request.GeneratedByAI.HasValue)
				query = query.Where(r => r.GeneratedByAI == request.GeneratedByAI.Value);

			return await query
				.OrderByDescending(r => r.GeneratedDate)
				.FirstOrDefaultAsync();
		}
	}
}
