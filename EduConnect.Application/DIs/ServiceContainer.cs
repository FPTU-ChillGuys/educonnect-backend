using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EduConnect.Application.DIs
{
	public static class ServiceContainer
	{
		
		public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
		{
			services.AddJWTAuthenticationScheme(config);
			return services;
		}

		public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
		{
			string jwtKey = config["Authentication:Key"] ?? throw new Exception("Missing JWT Key");
			string jwtIssuer = config["Authentication:Issuer"] ?? throw new Exception("Missing JWT Issuer");
			string jwtAudience = config["Authentication:Audience"] ?? throw new Exception("Missing JWT Audience");

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidIssuer = jwtIssuer,
						ValidAudience = jwtAudience,
						ValidateLifetime = true, // should validate expiration
						ClockSkew = TimeSpan.Zero // remove buffer time
					};
				});

			return services;
		}
	}
}
