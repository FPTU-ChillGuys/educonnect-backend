using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IClassRepository : IGenericRepository<Class>
	{
		Task<bool> IsHomeroomTeacherAsync(Guid userId, Guid classId);
		Task<Guid> GetClassIdByStudentIdAsync(Guid studentId);
	}
}
