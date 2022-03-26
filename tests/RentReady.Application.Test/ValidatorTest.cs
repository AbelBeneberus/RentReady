using FluentAssertions;
using RentReady.Application.Commands.CreateTimeEntry;
using System;
using Xunit;

namespace RentReady.Application.Test
{
	public class ValidatorTest
	{

		private CreateTimeEntryCommandValidator validator;
		public const string ValidationMessage = "Invalid date/time";

		public ValidatorTest()
		{
			validator = new CreateTimeEntryCommandValidator();
		}

		[Fact]
		public void GivenAllValidDate_WhenValidating_ShouldBeValid()
		{
			// arrange 
			CreateTimeEntryCommand createTimeEntryCommand = new CreateTimeEntryCommand()
			{
				EndOn = DateTime.Now.ToString(),
				StartOn = DateTime.Now.ToString()
			};

			// act

			var result = validator.Validate(createTimeEntryCommand);

			// assert

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeEmpty();
		}

		[Fact]
		public void GivenAllInValidDate_WhenValidating_ShouldBeInvalid()
		{
			// arrange 
			CreateTimeEntryCommand createTimeEntryCommand = new CreateTimeEntryCommand()
			{
				EndOn = String.Empty,
				StartOn = String.Empty
			};

			// act

			var result = validator.Validate(createTimeEntryCommand);

			// assert

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.Errors.Count.Should().Be(2);
		}

		[Fact]
		public void GivenInValidEndOnDateAndValidStartOnDate_WhenValidating_ShouldBeInvalid()
		{
			// arrange 
			CreateTimeEntryCommand createTimeEntryCommand = new CreateTimeEntryCommand()
			{
				EndOn = String.Empty,
				StartOn = DateTime.Now.ToString()
			};

			// act

			var result = validator.Validate(createTimeEntryCommand);

			// assert

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.Errors.Count.Should().Be(1);
		}
		
		[Fact]
		public void GivenInValidEndOnDateAndValidStartOnDate_WhenValidating_ShouldProvideValiMessage()
		{
			// arrange 
			CreateTimeEntryCommand createTimeEntryCommand = new CreateTimeEntryCommand()
			{
				EndOn = String.Empty,
				StartOn = DateTime.Now.ToString()
			};

			// act

			var result = validator.Validate(createTimeEntryCommand);

			// assert

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.Errors.Count.Should().Be(1);
		    result.Errors[0].ErrorMessage.Should().BeSameAs(ValidationMessage);
		}
	}
}