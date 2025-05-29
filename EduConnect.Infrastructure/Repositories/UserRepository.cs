using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Infrastructure.DBContext;
using Microsoft.Extensions.Configuration;

namespace EduConnect.Infrastructure.Repositories
{
	public class UserRepository(EduConnectDBContext _context, IConfiguration config) : IUserRepository
	{
		public Task<int> CreateAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<User>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<User> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public Task<int> UpdateAsync(User entity)
		{
			throw new NotImplementedException();
		}
		// Other methods from IGenericRepository<User> would be implemented here
	}
}
