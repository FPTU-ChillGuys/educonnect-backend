using AutoMapper;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.NotificationRequests;
using EduConnect.Application.DTOs.Responses.NotificationResponses;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IGenericRepository<Notification> _notificationRepository;
		private readonly IValidator<CreateNotificationRequest> _createNotificationRequestValidator;
		private readonly IMapper _mapper;

		public NotificationService(IGenericRepository<Notification> notificationRepository, IMapper mapper, IValidator<CreateNotificationRequest> createNotificationRequestValidator)
		{
			_notificationRepository = notificationRepository;
			_mapper = mapper;
			_createNotificationRequestValidator = createNotificationRequestValidator;
		}

		public async Task<BaseResponse<List<NotificationDto>>> GetNotificationsByUserIdAsync(Guid userId)
		{
			try
			{
				var notifications = await _notificationRepository.GetAllAsync(
					filter: n => n.RecipientUserId == userId,
					asNoTracking: true
				);

				var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);

				return BaseResponse<List<NotificationDto>>.Ok(notificationDtos, "Notifications retrieved successfully.");
			}
			catch (Exception ex)
			{
				return BaseResponse<List<NotificationDto>>.Fail("Failed to retrieve notifications.", new List<string> { ex.Message });
			}
		}

		public async Task<BaseResponse<NotificationDto>> CreateNotificationAsync(CreateNotificationRequest request)
		{
			var validationResult = await _createNotificationRequestValidator.ValidateAsync(request);
			if (!validationResult.IsValid)
				return BaseResponse<NotificationDto>.Fail(
					"Validation failed.",
					validationResult.Errors.Select(e => e.ErrorMessage).ToList()
				);

			try
			{
				// Convert current UTC time to Vietnam time (UTC+7)
				var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
				var sentAtVietnam = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

				var notification = new Notification
				{
					NotificationId = Guid.NewGuid(),
					Title = request.Title,
					Content = request.Content,
					RecipientUserId = request.RecipientUserId,
					ClassReportId = request.ClassReportId,
					StudentReportId = request.StudentReportId,
					SentAt = sentAtVietnam,
					IsRead = request.IsRead
				};

				await _notificationRepository.AddAsync(notification);
				await _notificationRepository.SaveChangesAsync();

				var notificationDto = _mapper.Map<NotificationDto>(notification);
				return BaseResponse<NotificationDto>.Ok(notificationDto, "Notification created successfully.");
			}
			catch (Exception ex)
			{
				return BaseResponse<NotificationDto>.Fail(
					"Failed to create notification.",
					new List<string> { ex.Message }
				);
			}
		}

		public async Task<BaseResponse<NotificationDto>> MarkNotificationAsReadAsync(Guid notificationId)
		{
			var notification = await _notificationRepository.GetByIdAsync(n => n.NotificationId == notificationId);
			if (notification == null)
				return BaseResponse<NotificationDto>.Fail("Notification not found.");

			notification.IsRead = true;
			_notificationRepository.Update(notification);
			await _notificationRepository.SaveChangesAsync();

			var notificationDto = _mapper.Map<NotificationDto>(notification);
			return BaseResponse<NotificationDto>.Ok(notificationDto, "Notification marked as read.");
		}
	}
}
