using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
			bool asNoTracking = false,
			int? skip = null,
			int? take = null
		);

		Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
			int pageNumber = 1,
			int pageSize = 10,
			bool asNoTracking = false
		);

		Task<T?> GetByIdAsync(Guid id);
		Task<T?> FirstOrDefaultAsync(
			Expression<Func<T, bool>> filter,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
			bool asNoTracking = false
		);

		Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		void Update(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
		Task<bool> SaveChangesAsync();
	}
}
