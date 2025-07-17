using DotNetEnv;
using EduConnect.ChatbotAPI.Configurations;
using EduConnect.ChatbotAPI.Extensions;
using EduConnect.ChatbotAPI.Hubs;
using EduConnect.ChatbotAPI.Services.Class;
using Hangfire;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

//Load.env files BEFORE configuration is built
string? solutionRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
string envFile = Path.Combine(solutionRootPath, ".env");
if (File.Exists(envFile)) Env.Load(envFile);

// Add config sources
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddChatbotServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add custom services
app.Lifetime.ApplicationStarted.Register(() =>
{
    var currentTimeUTC = DateTime.UtcNow.ToString();
    byte[] encodedCurrentTimeUTC = System.Text.Encoding.UTF8.GetBytes(currentTimeUTC);
    var options = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromHours(1));
    app.Services.GetService<IDistributedCache>()
                              .Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
});

app.UseHangfireDashboard();

app.MapHub<ChatbotHub>("/chatbot");

app.UseRegisteredHangfireJobs(classReportPlugin: app.Services.GetRequiredService<ClassReportService>());

app.Run();
