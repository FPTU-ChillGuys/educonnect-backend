using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infrastructure.Repositories
{
	public class ClassReportRepository : GenericRepository<ClassReport>, IClassReportRepository
	{
		public ClassReportRepository(EduConnectDbContext context) : base(context) { }

		public async Task<ClassReport?> GetLatestClassReportForNotificationAsync(GetClassReportToNotifyRequest request)
		{
			IQueryable<ClassReport> query = _context.ClassReports
				.Where(r => r.ClassId == request.ClassId);

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
