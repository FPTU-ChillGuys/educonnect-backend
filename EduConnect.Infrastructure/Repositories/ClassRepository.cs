using EduConnect.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using EduConnect.Persistence.Data;
using EduConnect.Domain.Entities;

namespace EduConnect.Infrastructure.Repositories
{
	public class ClassRepository : GenericRepository<Class>, IClassRepository
	{
		public ClassRepository(EduConnectDbContext context) : base(context) { }

		public async Task<bool> IsHomeroomTeacherAsync(Guid userId, Guid classId)
		{
			return await _context.Classes
				.AnyAsync(c => c.ClassId == classId && c.HomeroomTeacherId == userId);
		}

		public async Task<Guid> GetClassIdByStudentIdAsync(Guid studentId)
		{
			var classWithStudent = await _context.Classes
				.Where(c => c.Students.Any(s => s.StudentId == studentId))
				.Select(c => c.ClassId)
				.FirstOrDefaultAsync();

			return classWithStudent == Guid.Empty ? Guid.Empty : classWithStudent;
		}
	}
}
