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
using EduConnect.Infrastructure.Repositories;
using EduConnect.Infrastructure.Services;
using EduConnect.Persistence.Data;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

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

            //Add Service
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationService, ConversationService>();

            //Add Chatbot Services
            services.AddScoped<ChatbotStorage>();
            services.AddScoped<ChatbotHelper>();

            //Add Mapper
            services.AddAutoMapper(typeof(ConversationProfile).Assembly);

            //Add other services
            services.AddSingleton<HttpClient>();
            services.AddSignalR();
            services.AddLogging();

            // Add notification service
            services.AddHttpClient<IFcmNotificationService, FcmNotificationService>();

            services.AddInfrastructureService(config);
            services.AddApplicationServices();

            //Add singleton
            services.AddScoped<ClassReportService>();

            // Add hangfire service
            services.AddHangfire(config => {
                config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(connectionString);
            });

            services.AddHangfireServer();

            services.AddDistributedMemoryCache();
            services.AddSingleton<Kernel>(AddKernal(config));
            return services;
        }

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Register Repositories
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassReportRepository, ClassReportRepository>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IStudentReportRepository, StudentReportRepository>();
            services.AddScoped<IGenericRepository<Class>, GenericRepository<Class>>();
            services.AddScoped<IGenericRepository<Period>, GenericRepository<Period>>();
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IGenericRepository<ClassReport>, GenericRepository<ClassReport>>();
            services.AddScoped<IGenericRepository<ClassSession>, GenericRepository<ClassSession>>();
            services.AddScoped<IGenericRepository<StudentReport>, GenericRepository<StudentReport>>();
            services.AddScoped<IGenericRepository<ClassBehaviorLog>, GenericRepository<ClassBehaviorLog>>();
            services.AddScoped<IGenericRepository<StudentBehaviorNote>, GenericRepository<StudentBehaviorNote>>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();

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

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Services
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IBehaviorService, BehaviorService>();
            services.AddScoped<IClassSessionService, ClassSessionService>();
            services.AddScoped<INotificationJobService, NotificationJobService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IConversationService, ConversationService>();

            // AutoMapper
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddAutoMapper(typeof(ClassProfile).Assembly);
            services.AddAutoMapper(typeof(StudentProfile).Assembly);
            services.AddAutoMapper(typeof(SubjectProfile).Assembly);
            services.AddAutoMapper(typeof(BehaviorProfile).Assembly);
            services.AddAutoMapper(typeof(ClassSessionProfile).Assembly);

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
            services.AddScoped<IValidator<CreateStudentReportRequest>, CreateStudentReportRequestValidator>();
            services.AddScoped<IValidator<UpdateClassBehaviorLogRequest>, UpdateClassBehaviorLogRequestValidator>();
            services.AddScoped<IValidator<CreateClassBehaviorLogRequest>, CreateClassBehaviorLogRequestValidator>();
            services.AddScoped<IValidator<UpdateClassSessionByAdminRequest>, UpdateClassSessionByAdminRequestValidator>();
            services.AddScoped<IValidator<CreateStudentBehaviorNoteRequest>, CreateStudentBehaviorNoteRequestValidator>();
            services.AddScoped<IValidator<UpdateStudentBehaviorNoteRequest>, UpdateStudentBehaviorNoteRequestValidator>();

            return services;
        }

        #pragma warning disable SKEXP0070
        public static Kernel AddKernal(IConfiguration config)
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            //kernelBuilder.AddOllamaChatCompletion("qwen3:0.6b", new Uri("http://localhost:11434"));
            // Use OpenAI chat completion service
            kernelBuilder.AddGoogleAIGeminiChatCompletion(
                 modelId: "gemini-2.0-flash-lite",
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