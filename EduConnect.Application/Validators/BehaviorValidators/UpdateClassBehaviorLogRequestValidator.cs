using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.BehaviorValidators
{
	public class UpdateClassBehaviorLogRequestValidator : AbstractValidator<UpdateClassBehaviorLogRequest>
	{
		public UpdateClassBehaviorLogRequestValidator()
		{
			RuleFor(x => x.BehaviorType).NotEmpty().MaximumLength(100);
		}
	}
}
