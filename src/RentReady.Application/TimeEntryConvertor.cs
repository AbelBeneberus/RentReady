using RentReady.Application.Commands.CreateTimeEntry;

namespace RentReady.Application
{
	public static class TimeEntryConvertor
	{
		public static Domain.TimeEntry ConvertToDamainTimeEntry(TimeEntryDto timeEntry)
		{
			DateTime startOnDateTime = GetDateTimeFromString(timeEntry.StartOn);
			DateTime endOnDateTime = GetDateTimeFromString(timeEntry.EndOn);

			return new Domain.TimeEntry() {
				StartOn = startOnDateTime,
				EndOn = endOnDateTime,
				Duration = GetDurationInMinute(startOnDateTime, endOnDateTime)
			};
				 
		}

		private static DateTime GetDateTimeFromString(string date)
		{
			return DateTime.TryParse(date, out var dateTime)? dateTime : DateTime.MinValue; 
		}
		private static double GetDurationInMinute(DateTime startDate, DateTime endDate)
		{
			TimeSpan duration = endDate - startDate;
			return duration.TotalMinutes;
		}
	}
}
