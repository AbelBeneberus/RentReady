using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using RentReady.Application;
using RentReady.Domain;
using RentReady.Infrastructure.Authentiction;

namespace RentReady.Infrastructure
{
	public class DataVerseRepository : ITimeEntryRepository
	{
		private const string TimeEntryEntityName = "msdyn_timeentry";
		private readonly IClientBuilder _clientBuilder;
		private readonly ServiceClient crmServiceClient;
		private readonly ILogger<DataVerseRepository> _logger;

		public DataVerseRepository(IClientBuilder clientBuilder, ILogger<DataVerseRepository> logger)
		{
			_clientBuilder = clientBuilder;
			crmServiceClient = clientBuilder.GetServiceClient();
			_logger = logger;

		}

		public async Task CreateTimeEntryRecordAsync(TimeEntry timeEntry)
		{
			if (!IsTheServiceReady())
			{
				throw new Exception();
			}

			try
			{
				Entity newTimeEntry = new Entity(TimeEntryEntityName);

				newTimeEntry["msdyn_start"] = timeEntry.StartOn.ToString();
				newTimeEntry["msdyn_end"] = timeEntry.EndOn.ToString();
				newTimeEntry["msdyn_duration"] = timeEntry.Duration;
				 
				var recordId = await crmServiceClient.CreateAsync(newTimeEntry);
				
				if (recordId != null)
				{

				}
			}
			catch (Exception excption)
			{

				throw;
			}
			
		}
		private bool IsTheServiceReady()
		{
			return crmServiceClient.IsReady;
		}
	}
}
