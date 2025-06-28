using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.BehaviorValidators
{
	public class CreateClassBehaviorLogRequestValidator : AbstractValidator<CreateClassBehaviorLogRequest>
	{
		public CreateClassBehaviorLogRequestValidator()
		{
			RuleFor(x => x.ClassSessionId).NotEmpty();
			RuleFor(x => x.BehaviorType).NotEmpty().MaximumLength(100);
		}
	}
}
