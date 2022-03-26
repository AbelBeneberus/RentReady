using MediatR;

namespace RentReady.Application.Commands.CreateTimeEntry
{
	public class CreateTimeEntryCommandHandler : IRequestHandler<CreateTimeEntryCommand, Unit>
	{
		private readonly ITimeEntryRepository _timeEntryRepository;
		public CreateTimeEntryCommandHandler(ITimeEntryRepository timeEntryRepository)
		{
			_timeEntryRepository = timeEntryRepository;
		}
		public async Task<Unit> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
		{
			var timeEntryDto = new TimeEntryDto
			{
				StartOn = request.StartOn,
				EndOn = request.EndOn
			};

			var domainEntity = TimeEntryConvertor.ConvertToDamainTimeEntry(timeEntryDto);

			await _timeEntryRepository.CreateTimeEntryRecordAsync(domainEntity);
			return Unit.Value;
		}
	}
}
