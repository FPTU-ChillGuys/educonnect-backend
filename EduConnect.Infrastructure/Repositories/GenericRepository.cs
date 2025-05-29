//using EduConnect.Application.Interfaces.Repositories;
//using EduConnect.Infrastructure.DBContext;
//using Microsoft.EntityFrameworkCore;

//namespace EduConnect.Infrastructure.Repositories
//{
//	public class GenericRepository<T> : IGenericRepository<T> where T : class
//	{
//		protected readonly EduConnectDBContext _context;

//		public GenericRepository(EduConnectDBContext context)
//		{
//			_context = context;
//		}

//		public async Task<List<T>> GetAllAsync() =>
//			await _context.Set<T>().ToListAsync();

//		public async Task<T> GetByIdAsync(object id) =>
//			await _context.Set<T>().FindAsync(id);

//		public async Task<int> CreateAsync(T entity)
//		{
//			_context.Add(entity);
//			return await _context.SaveChangesAsync();
//		}

//		public async Task<int> UpdateAsync(T entity)
//		{
//			_context.Entry(entity).State = EntityState.Modified;
//			return await _context.SaveChangesAsync();
//		}

//		public async Task<bool> RemoveAsync(T entity)
//		{
//			_context.Remove(entity);
//			await _context.SaveChangesAsync();
//			return true;
//		}
//	}
//}
