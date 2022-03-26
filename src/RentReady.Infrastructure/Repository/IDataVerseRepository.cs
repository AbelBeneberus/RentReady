using RentReady.Domain;

namespace RentReady.Infrastructure
{
	public interface IDataVerseRepository	
	{
		Task CreateTimeEntryAsync(TimeEntry timeEntry, string authToken); 
	}
}