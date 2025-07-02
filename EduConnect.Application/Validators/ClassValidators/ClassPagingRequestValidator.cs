using EduConnect.Application.DTOs.Requests.ClassRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassValidators
{
	public class ClassPagingRequestValidator : AbstractValidator<ClassPagingRequest>
	{
		public ClassPagingRequestValidator()
		{
			RuleFor(x => x.PageNumber).GreaterThan(0);
			RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
			RuleFor(x => x.SortBy).Must(BeAValidSortField).When(x => !string.IsNullOrWhiteSpace(x.SortBy));
		}

		private bool BeAValidSortField(string? field)
		{
			return new[] { "ClassName", "GradeLevel", "AcademicYear" }.Contains(field);
		}
	}
}
