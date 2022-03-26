using FluentValidation;
using System;

namespace RentReady.Application.Commands.CreateTimeEntry
{
	public class CreateTimeEntryCommandValidator : AbstractValidator<CreateTimeEntryCommand>
	{
		public const string ValidationMessage = "Invalid date/time";
		public CreateTimeEntryCommandValidator()
		{
			RuleFor(createTimeEntryCommnad => createTimeEntryCommnad.StartOn)
				.Must(BeValidDate)
				.WithMessage($"{ValidationMessage} identified for {{startOn}} property");

			RuleFor(createTimeEntryCommnad => createTimeEntryCommnad.EndOn)
				.Must(BeValidDate)
				.WithMessage($"{ValidationMessage} identified for {{EndOn}} property");

			RuleFor(ctec => ConvertToDate(ctec.StartOn).ToShortDateString())
							   .NotEqual(DateTime.MinValue.ToShortDateString())
							   .WithMessage("StartOn date is required.")
			   .Equal(ctec => ConvertToDate(ctec.EndOn).ToShortDateString())
							   .WithMessage("StartOn and EndOn Dates should have to be in the same date.");

		}

		private bool BeValidDate(string stringDate)
		{
			return DateTime.TryParse(stringDate, out var date);
		}
		private DateTime ConvertToDate(string stringDate)
		{
			return DateTime.TryParse(stringDate, out var date) ? date : DateTime.MinValue;
		}
	}
}
