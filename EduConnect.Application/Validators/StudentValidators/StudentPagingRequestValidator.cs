using EduConnect.Application.DTOs.Requests.StudentRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.StudentValidators
{
	public class StudentPagingRequestValidator : AbstractValidator<StudentPagingRequest>
	{
		public StudentPagingRequestValidator()
		{
			RuleFor(x => x.SortBy)
				.Must(BeAValidSortField)
				.When(x => !string.IsNullOrWhiteSpace(x.SortBy))
				.WithMessage("Invalid sort field");

			RuleFor(x => x)
				.Must(x => !(x.ClassId.HasValue && x.ParentId.HasValue))
				.WithMessage("You can filter by either ClassId or ParentId, not both.");
		}

		private bool BeAValidSortField(string? sortBy)
		{
			var validFields = new[] { "FullName", "StudentCode", "Gender", "DateOfBirth" };
			return validFields.Contains(sortBy);
		}
	}
}
