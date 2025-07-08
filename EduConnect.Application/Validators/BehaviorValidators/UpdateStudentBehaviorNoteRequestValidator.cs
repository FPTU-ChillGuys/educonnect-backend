using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.BehaviorValidators
{
	public class UpdateStudentBehaviorNoteRequestValidator : AbstractValidator<UpdateStudentBehaviorNoteRequest>
	{
		public UpdateStudentBehaviorNoteRequestValidator()
		{
			RuleFor(x => x.BehaviorType).NotEmpty().MaximumLength(100);
		}
	}
}
