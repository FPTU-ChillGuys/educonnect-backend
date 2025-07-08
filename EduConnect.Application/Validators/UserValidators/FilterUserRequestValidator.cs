using EduConnect.Application.DTOs.Requests.UserRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.UserValidators
{
	public class FilterUserRequestValidator : AbstractValidator<FilterUserRequest>
	{
		public FilterUserRequestValidator()
		{
			RuleFor(x => x.Keyword).MaximumLength(100);
			RuleFor(x => x.Role)
				.Must(role => string.IsNullOrEmpty(role) || role == "Teacher" || role == "Admin" || role == "Parent")
				.WithMessage("Invalid role name"); 
			RuleFor(x => x.Subject).MaximumLength(50);
		}
	}
}
