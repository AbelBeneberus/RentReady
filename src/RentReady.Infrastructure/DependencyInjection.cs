using Microsoft.Extensions.DependencyInjection;
using RentReady.Application;
using RentReady.Infrastructure.Authentiction;

namespace RentReady.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddScoped<ITimeEntryRepository, DataVerseRepository>(); 
			services.AddScoped<IClientServiceProvider, ClientServiceProvider>(); 

			return services;
		}
	}
}
