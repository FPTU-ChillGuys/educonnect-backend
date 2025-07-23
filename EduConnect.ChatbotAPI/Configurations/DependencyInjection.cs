using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Mappings;
using EduConnect.Application.Services;
using EduConnect.Application.Validators.BehaviorValidators;
using EduConnect.Application.Validators.ClassSessionValidators;
using EduConnect.Application.Validators.ClassValidators;
using EduConnect.Application.Validators.ReportValidators;
using EduConnect.Application.Validators.StudentValidators;
using EduConnect.Application.Validators.SubjectValidators;
using EduConnect.Application.Validators.UserValidators;
using EduConnect.ChatbotAPI.Plugins;
using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.ChatbotAPI.Services.Class;
using EduConnect.Domain.Entities;
using EduConnect.Infrastructure.Authorization.Handlers;
using EduConnect.Infrastructure.Extensions;
using EduConnect.Infrastructure.Repositories;
using EduConnect.Infrastructure.Services;
using EduConnect.Persistence.Data;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;

namespace EduConnect.ChatbotAPI.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddChatbotServices(this IServiceCollection services, IConfiguration config)
        {
            //Add DBContext
            var connectionString = config["DATABASE_CONNECTION_STRING"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DATABASE_CONNECTION_STRING is not configured.");
            }

            services.AddDbContext<EduConnectDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Add Repo
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IClassReportRepository, ClassReportRepository>();
            services.AddScoped<IStudentReportRepository, StudentReportRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Add IConfiguration
            services.AddSingleton<IConfiguration>(config);

            //Add Service
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IBehaviorService, BehaviorService>();
            services.AddScoped<IClassSessionService, ClassSessionService>();
            services.AddScoped<ISupabaseStorageService, SupabaseStorageService>();

            //Add Chatbot Services
            services.AddScoped<ChatbotStorage>();
            services.AddScoped<ChatbotHelper>();

            // FluentValidation
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
            services.AddScoped<IValidator<CreateClassSessionRequest>, CreateClassSessionRequestValidator>();
            services.AddScoped<IValidator<UpdateClassSessionRequest>, UpdateClassSessionRequestValidator>();
			services.AddScoped<IValidator<ClassSessionPagingRequest>, ClassSessionPagingRequestValidator>();
            services.AddScoped<IValidator<CreateStudentReportRequest>, CreateStudentReportRequestValidator>();
            services.AddScoped<IValidator<UpdateClassBehaviorLogRequest>, UpdateClassBehaviorLogRequestValidator>();
            services.AddScoped<IValidator<CreateClassBehaviorLogRequest>, CreateClassBehaviorLogRequestValidator>();
            services.AddScoped<IValidator<UpdateClassSessionByAdminRequest>, UpdateClassSessionByAdminRequestValidator>();
            services.AddScoped<IValidator<CreateStudentBehaviorNoteRequest>, CreateStudentBehaviorNoteRequestValidator>();
            services.AddScoped<IValidator<UpdateStudentBehaviorNoteRequest>, UpdateStudentBehaviorNoteRequestValidator>();

			//Add Mapper
			services.AddAutoMapper(typeof(ConversationProfile).Assembly);

            // Add hangfire service
            services.AddHangfire(config => {
                config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(connectionString);
            });

            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { "default", "ReportQueue" };
            });

            //Add other services
            services.AddSingleton<HttpClient>();
            services.AddSignalR();
            services.AddLogging();

            services.AddDistributedMemoryCache();
            services.AddSingleton<Kernel>(AddKernal(config));
            return services;
        }

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Register Repositories
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IEmailTemplateProvider, MailTemplateProvider>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Class>, GenericRepository<Class>>();
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IGenericRepository<ClassSession>, GenericRepository<ClassSession>>();
            services.AddScoped<IGenericRepository<ClassBehaviorLog>, GenericRepository<ClassBehaviorLog>>();
            services.AddScoped<IGenericRepository<StudentBehaviorNote>, GenericRepository<StudentBehaviorNote>>();
            services.AddScoped<IClassReportRepository, ClassReportRepository>();
            services.AddScoped<IStudentReportRepository, StudentReportRepository>();

            // - DBContext
            var connectionString = config["DATABASE_CONNECTION_STRING"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DATABASE_CONNECTION_STRING is not configured.");
            }

            services.AddDbContext<EduConnectDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        #pragma warning disable SKEXP0070
        public static Kernel AddKernal(IConfiguration config)
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            //kernelBuilder.AddOllamaChatCompletion("qwen3:0.6b", new Uri("http://localhost:11434"));
            // Use OpenAI chat completion service

            if (string.IsNullOrWhiteSpace(config["GOOGLE_AI_API_KEY"]))
            {
                throw new InvalidOperationException("GOOGLE_AI_API_KEY");
            };


            kernelBuilder.AddGoogleAIGeminiChatCompletion(
                 modelId: "gemini-2.5-flash-lite-preview-06-17",
                 apiKey: config["GOOGLE_AI_API_KEY"] ?? ""
            );

            //Add plugins or additional services if needed
            kernelBuilder.Plugins.AddFromType<ClassPlugin>("Class");
            kernelBuilder.Plugins.AddFromType<StudentPlugin>("Student");
            kernelBuilder.Plugins.AddFromType<SubjectPlugin>("Subject");
            kernelBuilder.Plugins.AddFromType<BehaviorPlugin>("Behavior");
            kernelBuilder.Plugins.AddFromType<ClassSessionPlugin>("ClassSession");

            //Add other service
            kernelBuilder.Services.AddScoped<ChatbotStorage>();
            kernelBuilder.Services.AddSingleton<IConfiguration>(config);

            //Add logger
            kernelBuilder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });


            kernelBuilder.Services.AddApplicationServices();
            kernelBuilder.Services.AddInfrastructureService(config);

            kernelBuilder.Services.AddSingleton<HttpClient>();
            Kernel kernel = kernelBuilder.Build();
            return kernel;

        }
    }
}