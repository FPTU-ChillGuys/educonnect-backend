using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using Microsoft.AspNetCore.Identity;
using EduConnect.Domain.Entities;
using FluentValidation;
using OfficeOpenXml;

namespace EduConnect.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepo;
		private readonly UserManager<User> _userManager;
		private readonly IValidator<UpdateUserRequest> _validator;

		public UserService(IUserRepository userRepo, UserManager<User> userManager, IValidator<UpdateUserRequest> validator)
		{
			_userRepo = userRepo;
			_userManager = userManager;
			_validator = validator;
		}

		public async Task<BaseResponse<int>> CountHomeroomTeachersAsync()
		{
			try
			{
				var count = await _userRepo.CountHomeroomTeachersAsync();
				return BaseResponse<int>.Ok(count);
			}
			catch (Exception ex)
			{
				return BaseResponse<int>.Fail($"Failed to count homeroom teachers: {ex.Message}");
			}
		}

		public async Task<BaseResponse<int>> CountSubjectTeachersAsync()
		{
			try
			{
				var count = await _userRepo.CountSubjectTeachersAsync();
				return BaseResponse<int>.Ok(count);
			}
			catch (Exception ex)
			{
				return BaseResponse<int>.Fail($"Failed to count subject teachers: {ex.Message}");
			}
		}

		public async Task<BaseResponse<int>> CountTeachersAsync()
		{
			try
			{
				var count = await _userRepo.CountUsersInRoleAsync("Teacher");
				return BaseResponse<int>.Ok(count);
			}
			catch
			{
				return BaseResponse<int>.Fail("Failed to retrieve teacher count");
			}
		}

		public async Task<PagedResponse<UserDto>> GetPagedUsersAsync(UserFilterRequest request)
		{
			var (items, totalCount) = await _userRepo.GetPagedUsersAsync(request);

			var dtoList = items.Select(u => new UserDto
			{
				UserId = u.Id,
				FullName = u.UserName,
				Email = u.Email,
				PhoneNumber = u.PhoneNumber,
				IsHomeroomTeacher = u.HomeroomClasses.Any(),
				IsSubjectTeacher = u.TeachingSessions.Any()
			}).ToList();

			return PagedResponse<UserDto>.Ok(dtoList, totalCount, request.PageNumber, request.PageSize);
		}

		public async Task<BaseResponse<byte[]>> ExportUsersToExcelAsync(ExportUserRequest request)
		{
			try
			{
				var users = await _userRepo.GetUsersForExportAsync(request);

				if (!users.Any())
					return BaseResponse<byte[]>.Fail("No users found to export");

				ExcelPackage.License.SetNonCommercialOrganization("EduConnect");
				using var package = new ExcelPackage();
				var worksheet = package.Workbook.Worksheets.Add("Users");

				// Header
				worksheet.Cells[1, 1].Value = "Full Name";
				worksheet.Cells[1, 2].Value = "Email";
				worksheet.Cells[1, 3].Value = "Phone Number";
				worksheet.Cells[1, 4].Value = "Is Homeroom Teacher";
				worksheet.Cells[1, 5].Value = "Is Subject Teacher";

				int row = 2;
				foreach (var u in users)
				{
					worksheet.Cells[row, 1].Value = u.UserName;
					worksheet.Cells[row, 2].Value = u.Email;
					worksheet.Cells[row, 3].Value = u.PhoneNumber;
					worksheet.Cells[row, 4].Value = u.HomeroomClasses.Any() ? "Yes" : "No";
					worksheet.Cells[row, 5].Value = u.TeachingSessions.Any() ? "Yes" : "No";
					row++;
				}

				worksheet.Cells.AutoFitColumns();
				return BaseResponse<byte[]>.Ok(package.GetAsByteArray(), "Users exported successfully");
			}
			catch (Exception ex)
			{
				return BaseResponse<byte[]>.Fail($"An error occurred during export: {ex.Message}");
			}
		}

		public async Task<BaseResponse<string>> UpdateUserAsync(Guid id, UpdateUserRequest request)
		{
			var validation = await _validator.ValidateAsync(request);
			if (!validation.IsValid)
				return BaseResponse<string>.Fail(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
				return BaseResponse<string>.Fail("User not found");

			// Apply updates
			user.PhoneNumber = request.PhoneNumber;
			user.Address = request.Address;

			// You might want to update FullName, but IdentityUser doesn't have it by default.
			// Consider extending User with a FullName property if not done already
			user.UserName = request.FullName; // or add user.FullName if you added that field
			user.NormalizedUserName = request.FullName?.ToUpperInvariant(); // Normalize if needed

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return BaseResponse<string>.Fail("Failed to update user");

			return BaseResponse<string>.Ok("User updated successfully");
		}

		public async Task<BaseResponse<string>> UpdateUserStatsusAsync(Guid id, UpdateUserStatusRequest request)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
				return BaseResponse<string>.Fail("User not found");
			user.IsActive = request.IsActive;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return BaseResponse<string>.Fail("Failed to update user status");
			return BaseResponse<string>.Ok("User status updated successfully");
		}
	}
}
