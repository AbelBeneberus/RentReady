using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RentReady.Application;
using RentReady.Application.Exceptions;
using RentReady.Domain;
using RentReady.Infrastructure.Authentiction;

namespace RentReady.Infrastructure
{
	public class DataVerseRepository : ITimeEntryRepository
	{
		private const string TimeEntryEntityName = "msdyn_timeentry";
		private const string TimeEntryStartOn = "msdyn_start";
		private const string TimeEntryEndOn = "msdyn_end";
		private const string TimeEntryDuration = "msdyn_duration";
		private const string TimeEntryDate = "msdyn_date";
		private const string TimeEntryId = "msdyn_timeentryid";

		private readonly IClientServiceProvider _clientServiceProvider;	
		private readonly ServiceClient service;
		private readonly ILogger<DataVerseRepository> _logger;
		

		public DataVerseRepository(IClientServiceProvider clientServiceProvider, ILogger<DataVerseRepository> logger)
		{ 
			service = clientServiceProvider.GetServiceClient();
			_logger = logger;
			_clientServiceProvider = clientServiceProvider; 
		}

		public async Task<DateTime> CreateTimeEntryRecordAsync(TimeEntry timeEntry)
		{
			if (!_clientServiceProvider.IsServiceReady())
			{
				throw new ServiceIsNotReadyException(service.LastException);
			}

			try
			{

				Entity newTimeEntry = new Entity(TimeEntryEntityName);

				newTimeEntry[TimeEntryStartOn] = timeEntry.StartOn;
				newTimeEntry[TimeEntryEndOn] = timeEntry.EndOn;
				newTimeEntry[TimeEntryDuration] = timeEntry.Duration;

				var timeEntryId = await service.CreateAsync(newTimeEntry);

				_logger.LogInformation("Created {0} timeEntry for the date of {1}.", newTimeEntry.LogicalName, newTimeEntry[TimeEntryStartOn]);

				return timeEntry.StartOn;
			}
			catch (Exception excption)
			{
				_logger.LogError(excption, $"Error occured while trying to create timeEntry for the date of {timeEntry.StartOn}", timeEntry);
				throw new RemoteServiceException(excption);
			}

		}

		public async Task<IEnumerable<TimeEntry>> GetTimeEntryByDateRangeAsync(DateTime fromDate, DateTime toDate)
		{
			if (!_clientServiceProvider.IsServiceReady())
			{
				throw new ServiceIsNotReadyException(service.LastException);
			}

			QueryExpression timeEntryQuery = new QueryExpression
			{
				EntityName = TimeEntryEntityName,
				ColumnSet = new ColumnSet(true),
				Criteria = new FilterExpression
				{
					Conditions =
					{
						new ConditionExpression
						{
							AttributeName = TimeEntryDate,
							Operator = ConditionOperator.GreaterEqual,
							Values = { fromDate }
						},
						new ConditionExpression {
							AttributeName = TimeEntryDate,
							Operator = ConditionOperator.LessEqual,
							Values = { toDate }
						}

					}
				}
			};

			var entities = await service.RetrieveMultipleAsync(timeEntryQuery);
						
			return entities.Entities.Select(entity => new TimeEntry()
			{
				TimeEntryId = entity.GetAttributeValue<Guid>(TimeEntryId),
				StartOn = entity.GetAttributeValue<DateTime>(TimeEntryStartOn),
				EndOn = entity.GetAttributeValue<DateTime>(TimeEntryEndOn),
				Duration = entity.GetAttributeValue<int>(TimeEntryDuration)
			});
		}
		 
		private bool IsTheServiceReady()
		{
			return service.IsReady;
		}
	}
}
