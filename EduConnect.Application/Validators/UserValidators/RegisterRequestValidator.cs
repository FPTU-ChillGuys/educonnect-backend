using EduConnect.Application.DTOs.Requests.AuthRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.UserValidators
{
	public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
	{
		public RegisterRequestValidator()
		{
			// Username - Required, Vietnamese letters, numbers, underscores, 3-20 characters
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Please input username!")
				.Matches(@"^[\p{L}0-9_]{3,20}$")
				.WithMessage("Username can only contain letters (including Vietnamese), numbers, and underscores (3-20 chars).");

			// FullName - Optional, but if provided, must match pattern
			RuleFor(x => x.FullName)
				.NotEmpty().WithMessage("Please input full name!")
				.Matches(@"^[\p{L} ]{3,50}$").WithMessage("Full name must be 3-50 characters and only contain Vietnamese letters and spaces.");

			// Email - Required and must be valid email
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Please input email!")
				.EmailAddress().WithMessage("Invalid email address!");

			// Password - Required (you can add regex for stronger rules)
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Please input password!");
			// Optional: Add this if you want stricter rules:
			// .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
			// .WithMessage("Password must be at least 8 characters and include upper/lowercase letters and a number.");

			// ClientUri - Required
			RuleFor(x => x.ClientUri)
				.NotEmpty().WithMessage("Client URI is required!");
		}
	}
}
