using Microsoft.PowerPlatform.Dataverse.Client;

namespace RentReady.Infrastructure.Authentiction
{
	public interface IClientServiceProvider
	{		
		ServiceClient GetServiceClient(); 
		bool IsServiceReady ();
	}
}
