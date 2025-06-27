using EduConnect.ChatbotAPI.Services.Chatbot;
using Microsoft.SemanticKernel;

namespace EduConnect.ChatbotAPI.Configurations
{
    public class DependencyInjection
    {
        public IServiceCollection AddInfrastructureService(IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<ChatbotHelper>();
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