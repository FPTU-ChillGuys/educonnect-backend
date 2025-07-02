using AutoMapper;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace EduConnect.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepo;
		private readonly IGenericRepository<User> _genericRepo;
		private readonly UserManager<User> _userManager;
		private readonly IValidator<UpdateUserRequest> _updateValidator;
		private readonly IValidator<FilterUserRequest> _filterValidator;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepo,
			UserManager<User> userManager,
			IValidator<UpdateUserRequest> updateValidator,
			IValidator<FilterUserRequest> filterValidator,
			IGenericRepository<User> genericRepo,
			IMapper mapper)
		{
			_userRepo = userRepo;
			_userManager = userManager;
			_updateValidator = updateValidator;
			_filterValidator = filterValidator;
			_genericRepo = genericRepo;
			_mapper = mapper;
		}

		public async Task<BaseResponse<int>> CountHomeroomTeachersAsync()
		{
			try
			{
				var count = await _genericRepo.CountAsync(u => u.HomeroomClasses.Any() && u.IsActive);
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
				var count = await _genericRepo.CountAsync(u => u.TeachingSessions.Any() && u.IsActive);
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

		public async Task<PagedResponse<UserDto>> GetPagedUsersAsync(FilterUserRequest request)
		{
			var validationResult = await _filterValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return PagedResponse<UserDto>.Fail(errors, request.PageNumber, request.PageSize);
			}

			var (items, totalCount) = await _userRepo.GetPagedUsersAsync(request);

			var dtoList = _mapper.Map<List<UserDto>>(items);

			return PagedResponse<UserDto>.Ok(dtoList, totalCount, request.PageNumber, request.PageSize);
		}

		public async Task<BaseResponse<UserDto>> GetUserByIdAsync(Guid id)
		{
			var user = await _userManager.Users
				.Include(u => u.HomeroomClasses)
				.Include(u => u.TeachingSessions)
				.FirstOrDefaultAsync(u => u.Id == id);
			if (user == null)
				return BaseResponse<UserDto>.Fail("User not found");
			
			var dto = _mapper.Map<UserDto>(user);
			return BaseResponse<UserDto>.Ok(dto, "User retrieved successfully");
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
			var validation = await _updateValidator.ValidateAsync(request);
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
