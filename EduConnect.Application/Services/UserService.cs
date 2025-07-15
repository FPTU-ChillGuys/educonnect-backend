using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using Microsoft.AspNetCore.Identity;
using EduConnect.Domain.Entities;
using FluentValidation;
using OfficeOpenXml;
using AutoMapper;

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

		public async Task<PagedResponse<UserDto>> GetPagedUsersAsync(FilterUserRequest request)
		{
			var validationResult = await _filterValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
				return PagedResponse<UserDto>.Fail(errors, request.PageNumber, request.PageSize);
			}

			var (items, totalCount) = await _userRepo.GetPagedUsersAsync(request);

			return totalCount == 0
				? PagedResponse<UserDto>.Fail("No users found", request.PageNumber, request.PageSize)
				: PagedResponse<UserDto>.Ok(items, totalCount, request.PageNumber, request.PageSize, "Users retrieved successfully");
		}

		public async Task<BaseResponse<List<UserLookupDto>>> GetUserLookupAsync(FilterUserRequest request)
		{
			var users = await _userRepo.GetUserLookupAsync(request); // Updated to use the new centralized repo method

			return users.Any()
				? BaseResponse<List<UserLookupDto>>.Ok(users, "Users loaded successfully")
				: BaseResponse<List<UserLookupDto>>.Fail("No users found");
		}

		public async Task<BaseResponse<UserDto>> GetUserByIdAsync(Guid id)
		{
			var (user, roleName) = await _userRepo.GetUserWithRoleByIdAsync(id);

			if (user == null)
				return BaseResponse<UserDto>.Fail("User not found");

			var dto = _mapper.Map<UserDto>(user);
			dto.RoleName = roleName ?? "Unknown";

			return BaseResponse<UserDto>.Ok(dto, "User retrieved successfully");
		}

		public async Task<BaseResponse<byte[]>> ExportUsersToExcelAsync(FilterUserRequest request)
		{
			try
			{
				var users = await _userRepo.GetUsersForExportAsync(request);

				if (!users.Any())
					return BaseResponse<byte[]>.Fail("No users found to export");

				ExcelPackage.License.SetNonCommercialOrganization("EduConnect");
				using var package = new ExcelPackage();
				var worksheet = package.Workbook.Worksheets.Add("Users");

				// Header row
				worksheet.Cells[1, 1].Value = "Full Name";
				worksheet.Cells[1, 2].Value = "Email";
				worksheet.Cells[1, 3].Value = "Phone Number";
				worksheet.Cells[1, 4].Value = "Address";
				worksheet.Cells[1, 5].Value = "Role Name";
				worksheet.Cells[1, 6].Value = "Is Homeroom Teacher";
				worksheet.Cells[1, 7].Value = "Is Subject Teacher";
				worksheet.Cells[1, 8].Value = "Status";

				int row = 2;
				foreach (var u in users)
				{
					worksheet.Cells[row, 1].Value = u.FullName;
					worksheet.Cells[row, 2].Value = u.Email;
					worksheet.Cells[row, 3].Value = u.PhoneNumber;
					worksheet.Cells[row, 4].Value = u.Address;
					worksheet.Cells[row, 5].Value = u.RoleName;
					worksheet.Cells[row, 6].Value = u.IsHomeroomTeacher ? "Yes" : "No";
					worksheet.Cells[row, 7].Value = u.IsSubjectTeacher ? "Yes" : "No";
					worksheet.Cells[row, 8].Value = u.IsActive ? "Active" : "Inactive";
					row++;
				}

				worksheet.Cells[1, 1, row - 1, 8].AutoFitColumns();

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

			if (!string.IsNullOrEmpty(request.FullName))
			{
				user.FullName = request.FullName;
			}
			
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
