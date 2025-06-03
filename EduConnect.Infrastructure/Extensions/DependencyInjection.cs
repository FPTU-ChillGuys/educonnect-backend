using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using EduConnect.Infrastructure.Repositories;
using EduConnect.Infrastructure.Services;
using EduConnect.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduConnect.Infrastructure.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
		{
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IEmailTemplateProvider, MailTemplateProvider>();

			// - DBContext
			var connectionString = config["DATABASE_CONNECTION_STRING"];

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new InvalidOperationException("DATABASE_CONNECTION_STRING is not configured.");
			}

			services.AddDbContext<EduConnectDbContext>(options =>
				options.UseSqlServer(connectionString));

			// - Identity
			services.AddIdentity<User, IdentityRole<Guid>>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredUniqueChars = 1;
			})
			.AddEntityFrameworkStores<EduConnectDbContext>()
			.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				options.User.AllowedUserNameCharacters =
					"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+đĐăĂâÂêÊôÔơƠưƯ" +
					"áàảãạấầẩẫậắằẳẵặéèẻẽẹếềểễệíìỉĩịóòỏõọốồổỗộớờởỡợúùủũụứừửữựýỳỷỹỵ" +
					"ÁÀẢÃẠẤẦẨẪẬẮẰẲẴẶÉÈẺẼẸẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌỐỒỔỖỘỚỜỞỠỢÚÙỦŨỤỨỪỬỮỰÝỲỶỸỴ";
			});

			// - CORS
			var webUrl = config["Front-end:webUrl"] ?? throw new Exception("Missing web url!!");
			var mobileUrl = config["Front-end:mobileUrl"] ?? throw new Exception("Missing mobile url!!");
			services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", builder =>
				{
					builder
						.WithOrigins(webUrl, mobileUrl)
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials();
				});
			});

			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Register Services
			services.AddScoped<IAuthService, AuthService>();

			// AutoMapper
			//services.AddAutoMapper(typeof(MappingProfile).Assembly);

			// FluentValidation (if validators are here)
			//services.AddValidatorsFromAssembly(typeof(RegisterValidator).Assembly);

			return services;
		}
	}
}
