using MediatR;

namespace RentReady.Application.Commands.CreateTimeEntry
{
	public class CreateTimeEntryCommandHandler : IRequestHandler<CreateTimeEntryCommand, TimeEntryCreatedResponse>
	{
		private readonly ITimeEntryRepository _timeEntryRepository;
		public CreateTimeEntryCommandHandler(ITimeEntryRepository timeEntryRepository)
		{
			_timeEntryRepository = timeEntryRepository;
		}
		public async Task<TimeEntryCreatedResponse> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
		{
			// TODO: Use automapper for manging the mapping between request command and DTO
			DateTime startOnDateTime = GetDateTimeFromString(request.StartOn);
			DateTime endOnDateTime = GetDateTimeFromString(request.EndOn);

			// check if there is an existing record for a specific dates in the given range
			var timeEntryRecordsInTheGivenRange = await _timeEntryRepository
				.GetTimeEntryByDateRangeAsync(startOnDateTime, endOnDateTime);

			List<Task> createTimeRecordTasks = new List<Task>();


			foreach (DateTime date in EachDay(startOnDateTime, endOnDateTime))
			{
				if (timeEntryRecordsInTheGivenRange.Any(te => te.StartOn.Date == date.Date))
				{
					continue;
				}
				createTimeRecordTasks.Add(_timeEntryRepository.CreateTimeEntryRecordAsync(TimeEntryConvertor.ConvertToDamainTimeEntry(new TimeEntryDto()
				{
					StartOn = date,
					EndOn = date
				})));
			}

			await Task.WhenAll(createTimeRecordTasks);

			List<DateTime> recordedDates = new List<DateTime>();

			foreach (Task createTimeRecordTask in createTimeRecordTasks)
			{
				recordedDates.Add(((Task<DateTime>)createTimeRecordTask).Result);
			}
			return new TimeEntryCreatedResponse()
			{ 
				Dates = recordedDates,
			};

		}
		private static DateTime GetDateTimeFromString(string date)
		{
			return DateTime.TryParse(date, out var dateTime) ? dateTime : DateTime.MinValue;
		}

		private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
		{
			for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
				yield return day;
		}
	}
}
