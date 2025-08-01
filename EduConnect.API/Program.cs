﻿using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Authorization;
using EduConnect.Infrastructure.Extensions;
using EduConnect.Infrastructure.Services;
using EduConnect.API.Configurations;
using Microsoft.OpenApi.Models;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Load .env manually in Development
if (builder.Environment.IsDevelopment())
{
	string solutionRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
	string envFile = Path.Combine(solutionRootPath, ".env");

	if (File.Exists(envFile))
	{
		var lines = File.ReadAllLines(envFile);

		foreach (var line in lines)
		{
			if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
				continue;

			var split = line.Split('=', 2);
			if (split.Length != 2)
				continue;

			var key = split[0].Trim();
			var value = split[1].Trim();

			if (value.StartsWith("\"") && value.EndsWith("\""))
				value = value[1..^1];

			Environment.SetEnvironmentVariable(key, value);
		}
	}
}

// Add config sources
builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddJWTAuthenticationScheme(builder.Configuration);
builder.Services.AddApplicationServices();
builder.AddServiceDefaults();

// Policy Authorization
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ClassAccessPolicy", policy =>
		policy.Requirements.Add(new ClassAccessRequirement()));
});

// Add notification service
builder.Services.AddHttpClient<IFcmNotificationService, FcmNotificationService>();

// Force all routes to be lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "EduConnect API", Version = "v1" });

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter your JWT token here"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseRouting();

app.UseGlobalExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();

