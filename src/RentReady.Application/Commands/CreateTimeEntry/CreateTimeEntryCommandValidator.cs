using FluentValidation;

namespace RentReady.Application.Commands.CreateTimeEntry
{
	public class CreateTimeEntryCommandValidator : AbstractValidator<CreateTimeEntryCommand>
	{		 
		public readonly string InvalidStartOnDateMessage = "Invalid date/time identified for StartOn property";
		public readonly string InvalidEndOnDateMessage = "Invalid date/time identified for EndOn property";
		public readonly string InvalidDateDiffMessage = "EndOn date should have to be less than from StartOn date.";
		public CreateTimeEntryCommandValidator()
		{
			RuleFor(createTimeEntryCommnad => createTimeEntryCommnad.StartOn)
				.Must(BeValidDate)
				.WithMessage(InvalidStartOnDateMessage);

			RuleFor(createTimeEntryCommnad => createTimeEntryCommnad.EndOn)
				.Must(BeValidDate)
				.WithMessage(InvalidEndOnDateMessage);

			RuleFor(ctec => ConvertToDate(ctec.StartOn))
							   .NotEqual(DateTime.MinValue)
							   .WithMessage("StartOn date is required.")
			   .LessThanOrEqualTo(ctec => ConvertToDate(ctec.EndOn))
							   .WithMessage(InvalidDateDiffMessage);

		}

		private bool BeValidDate(string stringDate)
		{
			return DateTime.TryParse(stringDate, out _);
		}
		private DateTime ConvertToDate(string stringDate)
		{
			return DateTime.TryParse(stringDate, out var date) ? date : DateTime.MinValue;
		}
	}
}
