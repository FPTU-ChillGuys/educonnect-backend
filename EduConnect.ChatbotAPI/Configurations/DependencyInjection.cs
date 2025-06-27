using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Mappings;
using EduConnect.Application.Services;
using EduConnect.ChatbotAPI.Services.Chatbot;
using EduConnect.Infrastructure.Repositories;
using Microsoft.SemanticKernel;

namespace EduConnect.ChatbotAPI.Configurations
{
    public class DependencyInjection
    {
        public IServiceCollection AddApplicationServices(IServiceCollection services, IConfiguration config)
        {
            //Add Repositories
            services.AddScoped<IConversationRepository, ConversationRepository>();

            //Add Services
            services.AddScoped<IConversationService, ConversationService>();

            //Add Chatbot Services
            services.AddScoped<ChatbotStorage>();
            services.AddScoped<ChatbotHelper>();

            //Add Mapper
            services.AddAutoMapper(typeof(ConversationProfile).Assembly);

            services.AddDistributedMemoryCache();
            services.AddSingleton<Kernel>(AddKernal());
            return services; 
        }

        #pragma warning disable SKEXP0070
        public Kernel AddKernal()
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddOllamaChatCompletion("qwen3:0.6b", new Uri("http://localhost:11434"));
            kernelBuilder.Services.AddSingleton<HttpClient>();
            Kernel kernel = kernelBuilder.Build();
            return kernel;

        }
    }
}