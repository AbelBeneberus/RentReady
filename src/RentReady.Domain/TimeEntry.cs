namespace RentReady.Domain
{
	public class TimeEntry : BaseEntity
	{
		public Guid TimeEntryId { get; set; }
		public DateTime StartOn { get; set; }
		public DateTime EndOn { get; set; }
		public int Duration { get; set; }
	}

}