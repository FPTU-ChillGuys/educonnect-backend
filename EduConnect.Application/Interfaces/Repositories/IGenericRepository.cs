namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync();
		Task<T> GetByIdAsync(object id);
		Task<int> CreateAsync(T entity);
		Task<int> UpdateAsync(T entity);
		Task<bool> RemoveAsync(T entity);
	}
}
