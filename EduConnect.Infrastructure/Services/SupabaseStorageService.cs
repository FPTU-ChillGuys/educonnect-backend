using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace EduConnect.Infrastructure.Services
{
    public class SupabaseStorageService : ISupabaseStorageService
    {
        private readonly Supabase.Client _client;
        private readonly IConfiguration _config;

        public SupabaseStorageService(IConfiguration config)
        {
            _config = config;
            _client = new Supabase.Client(
                _config["Supabase:Url"],
                _config["Supabase:Key"]
            );
        }

        public async Task<string?> UploadFileAsync(Stream fileStream, string fileName, string bucket = "educonnect")
        {
            await _client.InitializeAsync();

            var storage = _client.Storage;
            var bucketRef = storage.From(bucket);

            // Read stream into byte array
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await fileStream.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            var response = await bucketRef.Upload(fileBytes, fileName);

            if (!string.IsNullOrEmpty(response))
            {
                // Return public URL
                return bucketRef.GetPublicUrl(fileName);
            }

            return null;
        }

		public async Task<string?> UploadStudentAvatarAsync(Guid studentId, Stream fileStream, string fileExtension)
		{
			await _client.InitializeAsync();

			string fileName = $"avatars/{studentId}.jpg"; 
			var storage = _client.Storage;
			var bucketRef = storage.From("educonnect");

			// Ensure old file is removed
			try
			{
				await bucketRef.Remove(fileName); // ignore if it doesn't exist
			}
			catch (Exception ex)
			{
				// Optional: log warning but don't block upload
				Console.WriteLine("Failed to remove existing avatar: " + ex.Message);
			}

			using var ms = new MemoryStream();
			await fileStream.CopyToAsync(ms);
			var fileBytes = ms.ToArray();

			var response = await bucketRef.Upload(fileBytes, fileName);

			if (!string.IsNullOrEmpty(response))
				return bucketRef.GetPublicUrl(fileName);

			return null;
		}
	}
}