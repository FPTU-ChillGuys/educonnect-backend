using EduConnect.Application.DTOs.Requests.UserRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.UserValidators
{
	public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
	{
		public UpdateUserRequestValidator()
		{
			RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
			RuleFor(x => x.PhoneNumber).Matches(@"^[0-9]{10,15}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber));
			RuleFor(x => x.Address).MaximumLength(200);
		}
	}
}
