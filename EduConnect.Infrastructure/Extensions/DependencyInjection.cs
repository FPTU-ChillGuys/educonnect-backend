using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.Validators.ClassSessionValidators;
using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Validators.BehaviorValidators;
using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.Validators.StudentValidators;
using EduConnect.Application.Validators.SubjectValidators;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Validators.ReportValidators;
using EduConnect.Application.Validators.ClassValidators;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Validators.UserValidators;
using EduConnect.Infrastructure.Authorization.Handlers;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using EduConnect.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using EduConnect.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using EduConnect.Application.Mappings;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EduConnect.Persistence.Data;
using EduConnect.Domain.Entities;
using FluentValidation;
using Hangfire;
using EduConnect.Application.DTOs.Requests.AuthRequests;
using EduConnect.Application.DTOs.Requests.NotificationRequests;
using EduConnect.Application.Validators.NotificationValidators;

namespace EduConnect.Infrastructure.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
		{
			// Register Repositories
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IClassRepository, ClassRepository>();
			services.AddScoped<IMessageRepository, MessageRepository>();	
			services.AddScoped<IEmailTemplateProvider, MailTemplateProvider>();
			services.AddScoped<IClassReportRepository, ClassReportRepository>();
			services.AddScoped<IConversationRepository, ConversationRepository>();
			services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
			services.AddScoped<IStudentReportRepository, StudentReportRepository>();
			services.AddScoped<IGenericRepository<Class>, GenericRepository<Class>>();
			services.AddScoped<IGenericRepository<Period>, GenericRepository<Period>>();
			services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
			services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
			services.AddScoped<IGenericRepository<ClassReport>, GenericRepository<ClassReport>>();	
			services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();
			services.AddScoped<IGenericRepository<ClassSession>, GenericRepository<ClassSession>>();
			services.AddScoped<IGenericRepository<StudentReport>, GenericRepository<StudentReport>>();
			services.AddScoped<IGenericRepository<ClassBehaviorLog>, GenericRepository<ClassBehaviorLog>>();
			services.AddScoped<IGenericRepository<StudentBehaviorNote>, GenericRepository<StudentBehaviorNote>>();

			// Authorization Handlers
			services.AddScoped<IAuthorizationHandler, ClassAccessHandler>();

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

				options.User.RequireUniqueEmail = true;
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

			// Add hangfire service
			services.AddHangfire(config => {
				config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(connectionString);
			});
			services.AddHangfireServer(options => {
				options.Queues = new[] { "default" ,"SendNotificationQueue" };
			});

			//Add job schedule for notification
			services.AddHostedService<StartupNotificationJobScheduler>();

			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Register Services
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IClassService, ClassService>();
			services.AddScoped<IPeriodService, PeriodService>();
			services.AddScoped<IReportService, ReportService>();
			services.AddScoped<IMessageService, MessageService>();
			services.AddScoped<ISubjectService, SubjectService>();
			services.AddScoped<IStudentService, StudentService>();
			services.AddScoped<IBehaviorService, BehaviorService>();
			services.AddScoped<INotificationService, NotificationService>();
			services.AddScoped<IConversationService, ConversationService>();
			services.AddScoped<IClassSessionService, ClassSessionService>();
			services.AddScoped<INotificationJobService, NotificationJobService>();
			services.AddScoped<ISupabaseStorageService, SupabaseStorageService>();

			// AutoMapper
			services.AddAutoMapper(typeof(UserProfile).Assembly);
			services.AddAutoMapper(typeof(ClassProfile).Assembly);
			services.AddAutoMapper(typeof(StudentProfile).Assembly); 
			services.AddAutoMapper(typeof(SubjectProfile).Assembly);
			services.AddAutoMapper(typeof(BehaviorProfile).Assembly);
			services.AddAutoMapper(typeof(NotificationProfile).Assembly);
			services.AddAutoMapper(typeof(ClassSessionProfile).Assembly);

			// FluentValidation
			services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
			services.AddScoped<IValidator<TimetableRequest>, TimetableRequestValidator>();	
			services.AddScoped<IValidator<FilterUserRequest>, FilterUserRequestValidator>();
			services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
			services.AddScoped<IValidator<CreateClassRequest>, CreateClassRequestValidator>();
			services.AddScoped<IValidator<UpdateClassRequest>, UpdateClassRequestValidator>();
			services.AddScoped<IValidator<ClassPagingRequest>, ClassPagingRequestValidator>();
			services.AddScoped<IValidator<CreateStudentRequest>, CreateStudentRequestValidator>();
			services.AddScoped<IValidator<UpdateStudentRequest>, UpdateStudentRequestValidator>();
			services.AddScoped<IValidator<CreateSubjectRequest>, CreateSubjectRequestValidator>();
			services.AddScoped<IValidator<StudentPagingRequest>, StudentPagingRequestValidator>();
			services.AddScoped<IValidator<UpdateSubjectRequest>, UpdateSubjectRequestValidator>();
			services.AddScoped<IValidator<CreateClassReportRequest>, CreateClassReportRequestValidator>();
			services.AddScoped<IValidator<ClassSessionPagingRequest>, ClassSessionPagingRequestValidator>();
			services.AddScoped<IValidator<CreateNotificationRequest>, CreateNotificationRequestValidator>();	
			services.AddScoped<IValidator<CreateClassSessionRequest>, CreateClassSessionRequestValidator>();
			services.AddScoped<IValidator<UpdateClassSessionRequest>, UpdateClassSessionRequestValidator>();
			services.AddScoped<IValidator<CreateStudentReportRequest>, CreateStudentReportRequestValidator>();
			services.AddScoped<IValidator<UpdateClassBehaviorLogRequest>, UpdateClassBehaviorLogRequestValidator>();
			services.AddScoped<IValidator<CreateClassBehaviorLogRequest>, CreateClassBehaviorLogRequestValidator>();
			services.AddScoped<IValidator<UpdateClassSessionByAdminRequest>, UpdateClassSessionByAdminRequestValidator>();
			services.AddScoped<IValidator<CreateStudentBehaviorNoteRequest>, CreateStudentBehaviorNoteRequestValidator>();
			services.AddScoped<IValidator<UpdateStudentBehaviorNoteRequest>, UpdateStudentBehaviorNoteRequestValidator>();

			return services;
		}
	}
}
