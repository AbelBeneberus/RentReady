using RentReady.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace RentReady.Infrastructure.Authentiction
{
	public interface IClientBuilder
	{		
		ServiceClient GetServiceClient(); 
	}
}
