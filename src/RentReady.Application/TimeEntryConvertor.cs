using RentReady.Application.Commands.CreateTimeEntry;

namespace RentReady.Application
{
	public static class TimeEntryConvertor
	{
		public static Domain.TimeEntry ConvertToDamainTimeEntry(TimeEntryDto timeEntry)
		{
			return new Domain.TimeEntry()
			{
				StartOn = timeEntry.StartOn.Date + new TimeSpan(2,0,0),
				EndOn = timeEntry.EndOn.Date + new TimeSpan(11,0,0),
				Duration = GetDurationInMinute(timeEntry.StartOn, timeEntry.EndOn)
			};

		}

		private static int GetDurationInMinute(DateTime startDate, DateTime endDate)
		{
			TimeSpan duration = endDate - startDate;
			return Convert.ToInt32(duration.TotalSeconds);
		}
	}
}
