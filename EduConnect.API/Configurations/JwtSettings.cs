using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EduConnect.API.Configurations
{
	public static class JwtSettings
	{
		public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
		{
			string jwtKey = config["Authentication:Key"] ?? throw new Exception("Missing JWT Key");
			string jwtIssuer = config["Authentication:Issuer"] ?? throw new Exception("Missing JWT Issuer");
			string jwtAudience = config["Authentication:Audience"] ?? throw new Exception("Missing JWT Audience");

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(config["Authentication:Key"] ?? throw new Exception("Missing Key"))),
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidIssuer = config["Authentication:Issuer"],
						ValidAudience = config["Authentication:Audience"],
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					};
				});

			return services;
		}
	}
}
