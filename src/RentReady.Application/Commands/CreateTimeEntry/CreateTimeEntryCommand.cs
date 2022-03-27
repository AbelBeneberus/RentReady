using MediatR;

namespace RentReady.Application.Commands.CreateTimeEntry
{
	public class CreateTimeEntryCommand : IRequest<TimeEntryCreatedResponse>
	{
		public string StartOn { get; set; } = string.Empty;
		public string EndOn { get; set; } = string.Empty;
	}
}
