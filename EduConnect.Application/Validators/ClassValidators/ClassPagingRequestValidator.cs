using EduConnect.Application.DTOs.Requests.ClassRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassValidators
{
	public class ClassPagingRequestValidator : AbstractValidator<ClassPagingRequest>
	{
		public ClassPagingRequestValidator()
		{
			RuleFor(x => x.SortBy)
				.Must(BeAValidSortField)
				.When(x => !string.IsNullOrWhiteSpace(x.SortBy));

			RuleFor(x => x)
				.Must(x => !(x.TeacherId.HasValue && x.StudentId.HasValue))
				.WithMessage("You can filter by either TeacherId or StudentId, not both.");
		}

		private bool BeAValidSortField(string? field)
		{
			return new[] { "ClassName", "GradeLevel", "AcademicYear" }.Contains(field);
		}
	}
}
