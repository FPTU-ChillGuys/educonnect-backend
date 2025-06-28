using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.EduConnect_API>("api")
				 .WithEnvironment("DATABASE_CONNECTION_STRING", builder.Configuration["DATABASE_CONNECTION_STRING"])
				 .WithExternalHttpEndpoints(); // Add this line

var chatbotApi = builder.AddProject<Projects.EduConnect_ChatbotAPI>("chatbot-api")
				 .WithEnvironment("DATABASE_CONNECTION_STRING", builder.Configuration["DATABASE_CONNECTION_STRING"])
				 .WithExternalHttpEndpoints(); // Add this line

builder.Build().Run();
