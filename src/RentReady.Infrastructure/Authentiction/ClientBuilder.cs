using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentReady.Infrastructure.Authentiction
{
	public class ClientBuilder : IClientBuilder
	{
		private readonly ILogger<ClientBuilder> _logger;
		private readonly DataVerseConfiguration _config; 
		public ClientBuilder(ILogger<ClientBuilder> logger, DataVerseConfiguration configuration)
		{
			_logger = logger;
			_config = configuration;
		}

		public ServiceClient GetServiceClient()
		{
			return new ServiceClient(_config.ConnectionString, _logger); 
		}

	
	}
}
