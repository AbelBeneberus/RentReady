namespace RentReady.Domain
{
	public class TimeEntry : BaseEntity
	{
		public DateTime StartOn { get; set; }
		public DateTime EndOn { get; set; }
		public double Duration { get; set; }
	}

}