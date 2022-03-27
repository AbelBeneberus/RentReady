using FluentAssertions;
using MediatR;
using Moq;
using RentReady.Application.Commands.CreateTimeEntry;
using RentReady.Domain;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RentReady.Application.Test
{
	public class CreateTimeEntryCommandTest
	{
		[Fact]
		public async Task CrateTimeEntryCommand_CreateRecordOnDataVerseAsync()
		{
			//Arange
			var timeEntryRepository = new Mock<ITimeEntryRepository>();
			timeEntryRepository.Setup(ter => ter.CreateTimeEntryRecordAsync(It.IsAny<TimeEntry>()))
				.ReturnsAsync(It.IsAny<DateTime>);

			CreateTimeEntryCommand command = new CreateTimeEntryCommand()
			{
				EndOn = DateTime.Today.ToString(),
				StartOn = DateTime.Today.ToString()
			};

			CreateTimeEntryCommandHandler handler = new CreateTimeEntryCommandHandler(timeEntryRepository.Object);

			//Act
			var response = await handler.Handle(command, new System.Threading.CancellationToken());

			//Asert
			timeEntryRepository.Verify(x => x.CreateTimeEntryRecordAsync(It.IsAny<TimeEntry>()), Times.Once);
			timeEntryRepository.Verify(x => x.GetTimeEntryByDateRangeAsync(It.IsAny<DateTime>(),
				It.IsAny<DateTime>()), Times.Once);
			response.Dates.Should().Contain(It.IsAny<DateTime>());
		}

		[Fact]
		public async Task CrateDuplicateTimeEntryCommand_SkipDuplicatedEntryAsync()
		{
			//Arange
			var timeEntryRepository = new Mock<ITimeEntryRepository>();
			var timeEntries = new[]
			{
				new TimeEntry()
				{
					StartOn = DateTime.Today
				}
			};

			timeEntryRepository.Setup(ter => ter.CreateTimeEntryRecordAsync(It.IsAny<TimeEntry>()))
				.ReturnsAsync(It.IsAny<DateTime>);
			timeEntryRepository.Setup(ter => ter.GetTimeEntryByDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
				.ReturnsAsync(timeEntries);

			CreateTimeEntryCommand command = new CreateTimeEntryCommand()
			{
				EndOn = DateTime.Today.ToString(),
				StartOn = DateTime.Today.ToString()
			};

			CreateTimeEntryCommandHandler handler = new CreateTimeEntryCommandHandler(timeEntryRepository.Object);

			//Act
			var response = await handler.Handle(command, new System.Threading.CancellationToken());

			//Asert
			timeEntryRepository.Verify(x => x.CreateTimeEntryRecordAsync(It.IsAny<TimeEntry>()), Times.Never);
			timeEntryRepository.Verify(x => x.GetTimeEntryByDateRangeAsync(It.IsAny<DateTime>(),
				It.IsAny<DateTime>()), Times.Once);
			response.Dates.Should().BeEmpty();
		}
	}
}
