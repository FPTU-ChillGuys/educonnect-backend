using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using EduConnect.Infrastructure.DBContext;
using EduConnect.Infrastructure.Repositories;
using EduConnect.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduConnect.Infrastructure.DIs
{
	public static class ServiceContainer
	{
		// Only things like:
		// - DBContext
		// - Repositories
		// - 3rd-party integrations
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IAuthService, AuthService>();

			// - DBContext
			var connectionString = configuration["DATABASE_CONNECTION_STRING"];

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new InvalidOperationException("DATABASE_CONNECTION_STRING is not configured.");
			}

			services.AddDbContext<EduConnectDBContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<EduConnectDBContext>()
				.AddDefaultTokenProviders();

			return services;
		}
	}
}
