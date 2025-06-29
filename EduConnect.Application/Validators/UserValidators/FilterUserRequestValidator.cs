using EduConnect.Application.DTOs.Requests.UserRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.UserValidators
{
	public class FilterUserRequestValidator : AbstractValidator<FilterUserRequest>
	{
		public FilterUserRequestValidator()
		{
			RuleFor(x => x.Keyword).MaximumLength(100);
			RuleFor(x => x.Role).Must(r => r == "Teacher" || r == "Admin" || r == "Parent").WithMessage("Invalid role name");
			RuleFor(x => x.Subject).MaximumLength(50);
		}
	}
}
