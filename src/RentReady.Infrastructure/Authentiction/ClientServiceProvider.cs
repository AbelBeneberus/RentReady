using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using RentReady.Application.Exceptions;

namespace RentReady.Infrastructure.Authentiction
{
	public class ClientServiceProvider : IClientServiceProvider
	{
		private readonly ILogger<ClientServiceProvider> _logger;
		private readonly DataVerseConfiguration _config;
		private ServiceClient? _serviceClient;
		public ClientServiceProvider(ILogger<ClientServiceProvider> logger, DataVerseConfiguration configuration)
		{
			_logger = logger;
			_config = configuration;
		}

		public ServiceClient GetServiceClient()
		{
			try
			{
				_serviceClient = new ServiceClient(_config.ConnectionString, _logger);
				return _serviceClient;
			}
			catch (Exception ex)
			{
				throw new UnableToInizializeServiceClient(ex); 
			}
		
		}

		public bool IsServiceReady()
		{
			return _serviceClient != null && _serviceClient.IsReady;
		}
	}
}
