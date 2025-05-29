using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;

namespace EduConnect.Infrastructure.Services
{
	public class GenericService<T> : IGenericService<T> where T : class
	{
		private readonly IGenericRepository<T> _repository;

		public GenericService(IGenericRepository<T> repository)
		{
			_repository = repository;
		}

		public async Task<List<T>> GetAllAsync() => await _repository.GetAllAsync();

		public async Task<T> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);

		public async Task<int> CreateAsync(T entity) => await _repository.CreateAsync(entity);

		public async Task<int> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

		public async Task<bool> RemoveAsync(T entity) => await _repository.RemoveAsync(entity);
	}
}
