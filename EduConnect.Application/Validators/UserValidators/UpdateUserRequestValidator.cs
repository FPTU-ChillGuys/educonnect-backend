using EduConnect.Application.DTOs.Requests.UserRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.UserValidators
{
	public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
	{
		public UpdateUserRequestValidator()
		{
			RuleFor(x => x.FullName)
				.NotEmpty().WithMessage("Please input full name!")
				.Matches(@"^[\p{L} ]{3,50}$").WithMessage("Full name must be 3-50 characters and only contain Vietnamese letters and spaces.");
			RuleFor(x => x.PhoneNumber).Matches(@"^[0-9]{10,15}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber));
			RuleFor(x => x.Address).MaximumLength(200);
		}
	}
}
