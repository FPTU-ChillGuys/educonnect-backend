namespace EduConnect.Application.Interfaces.Services
{
	public interface ISupabaseStorageService
	{
		Task<string?> UploadFileAsync(Stream fileStream, string fileName, string bucket = "educonnect");
		Task<string?> UploadStudentAvatarAsync(Guid studentId, Stream fileStream, string fileExtension);
	}
}
