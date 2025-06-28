using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.Validators.ClassSessionValidators;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Validators.StudentValidators;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Validators.ClassValidators;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Validators.UserValidators;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using EduConnect.Infrastructure.Repositories;
using EduConnect.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using EduConnect.Application.Mappings;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EduConnect.Persistence.Data;
using EduConnect.Domain.Entities;
using FluentValidation;

namespace EduConnect.Infrastructure.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
		{
			// Register Repositories
			services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
			services.AddScoped<IGenericRepository<Class>, GenericRepository<Class>>();
			services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
			services.AddScoped<IGenericRepository<ClassSession>, GenericRepository<ClassSession>>();
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IEmailTemplateProvider, MailTemplateProvider>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
			services.AddScoped<IConversationService, ConversationService>();


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

			services.Configure<DataProtectionTokenProviderOptions>(options =>
			{
				options.TokenLifespan = TimeSpan.FromHours(24); // Set token lifespan to 24 hours
			});

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
			services.AddScoped<IStudentService, StudentService>();
			services.AddScoped<IClassService, ClassService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IClassSessionService, ClassSessionService>();

			// AutoMapper
			services.AddAutoMapper(typeof(StudentProfile).Assembly); 
			services.AddAutoMapper(typeof(ClassProfile).Assembly);
			services.AddAutoMapper(typeof(ClassSessionProfile).Assembly);

			// FluentValidation
			services.AddScoped<IValidator<CreateStudentRequest>, CreateStudentRequestValidator>();
			services.AddScoped<IValidator<UpdateStudentRequest>, UpdateStudentRequestValidator>();
			services.AddScoped<IValidator<CreateClassRequest>, CreateClassRequestValidator>();
			services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
			services.AddScoped<IValidator<CreateClassSessionRequest>, CreateClassSessionRequestValidator>();
			services.AddScoped<IValidator<UpdateClassSessionRequest>, UpdateClassSessionRequestValidator>();
			services.AddScoped<IValidator<UpdateClassSessionByAdminRequest>, UpdateClassSessionByAdminRequestValidator>();

			return services;
		}
	}
}
