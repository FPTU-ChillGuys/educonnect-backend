using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Google.Apis.Auth.OAuth2;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EduConnect.Infrastructure.Services
{
	public class FcmNotificationService : IFcmNotificationService
	{
		private readonly ILogger<FcmNotificationService> _logger;
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _config;
		private readonly string _projectId;

		public FcmNotificationService(HttpClient httpClient, ILogger<FcmNotificationService> logger, IConfiguration config)
		{
			_httpClient = httpClient;
			_logger = logger;
			_config = config;
			_projectId = _config["Fcm:ProjectId"] ?? throw new Exception("Missing FCM project id");
		}

		public async Task SendNotificationAsync(string deviceToken, string title, string body)
		{
			// 1. Load credentials
			var json = _config["Fcm:CredentialsJson"];
			if (string.IsNullOrWhiteSpace(json))
				throw new Exception("Missing FCM credentials JSON");
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
			var googleCredential = GoogleCredential.FromStream(stream)
				.CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

			// 2. Get access token
			var accessToken = await googleCredential.UnderlyingCredential
				.GetAccessTokenForRequestAsync();

			// 3. Build message payload
			var message = new
			{
				message = new
				{
					token = deviceToken,
					notification = new
					{
						title = title,
						body = body
					}
				}
			};

			var request = new HttpRequestMessage(HttpMethod.Post, $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send")
			{
				Headers =
			{
				Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
			},
				Content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json")
			};

			var response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				_logger.LogError($"FCM error: {error}");
			}
		}
	}
}
