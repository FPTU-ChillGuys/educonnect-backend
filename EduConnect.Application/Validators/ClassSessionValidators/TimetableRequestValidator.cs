using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassSessionValidators
{
	public class TimetableRequestValidator : AbstractValidator<TimetableRequest>
	{
		public TimetableRequestValidator()
		{
			RuleFor(x => x.TargetId)
				.NotEmpty()
				.WithMessage("Target ID must not be empty.");

			RuleFor(x => x.Mode)
				.Must(mode => mode == "Class" || mode == "Teacher")
				.WithMessage("Mode must be either 'Class' or 'Teacher'.");

			RuleFor(x => x.From)
				.LessThanOrEqualTo(x => x.To)
				.When(x => x.From != default && x.To != default)
				.WithMessage("From date must be earlier than or equal to To date.");
		}
	}
}
