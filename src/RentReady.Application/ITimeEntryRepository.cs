using RentReady.Domain;

namespace RentReady.Application
{
	public interface ITimeEntryRepository
	{
		Task<DateTime> CreateTimeEntryRecordAsync(TimeEntry timeEntry);
		Task<IEnumerable<TimeEntry>> GetTimeEntryByDateRangeAsync(DateTime fromDate, DateTime toDate);
	}
}
