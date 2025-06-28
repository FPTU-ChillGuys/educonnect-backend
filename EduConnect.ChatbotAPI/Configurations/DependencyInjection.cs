using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Mappings;
using EduConnect.Application.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.Infrastructure.Repositories;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

namespace EduConnect.ChatbotAPI.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //Add DBContext
            var connectionString = config["DATABASE_CONNECTION_STRING"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DATABASE_CONNECTION_STRING is not configured.");
            }

            services.AddDbContext<EduConnectDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Add Repositories
            services.AddScoped<IConversationRepository, ConversationRepository>();

            //Add Services
            services.AddScoped<IConversationService, ConversationService>();

            //Add Chatbot Services
            services.AddScoped<ChatbotStorage>();
            services.AddScoped<ChatbotHelper>();

            //Add Mapper
            services.AddAutoMapper(typeof(ConversationProfile).Assembly);

            //Add other services
            services.AddSingleton<HttpClient>();

            services.AddDistributedMemoryCache();
            services.AddSingleton<Kernel>(AddKernal());
            return services; 
        }

        #pragma warning disable SKEXP0070
        public static Kernel AddKernal()
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddOllamaChatCompletion("qwen3:0.6b", new Uri("http://localhost:11434"));
            kernelBuilder.Services.AddSingleton<HttpClient>();
            Kernel kernel = kernelBuilder.Build();
            return kernel;

        }
    }
}