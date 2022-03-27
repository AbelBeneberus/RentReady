using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentReady.Application;
using RentReady.Application.Behaviours;
using RentReady.Application.Commands.CreateTimeEntry;
using RentReady.Functions.Helpers;
using RentReady.Infrastructure;
using System.IO;

[assembly: FunctionsStartup(typeof(RentReady.Functions.Startup))]
namespace RentReady.Functions
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{

			var serviceCollection = builder.Services;
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("func.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			DataVerseConfiguration dataVerseConfiguration = config
				.GetSection("DataVerseConnection")
				.Get<DataVerseConfiguration>();


			serviceCollection.AddHttpClient();
			serviceCollection.AddMediatR(typeof(CreateTimeEntryCommand));
			serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			serviceCollection.AddSingleton<IValidator<CreateTimeEntryCommand>, CreateTimeEntryCommandValidator>();
			serviceCollection.AddSingleton<IHttpFunctionExecutor, HttpFunctionExecutor>();

			serviceCollection.AddSingleton(dataVerseConfiguration);


			serviceCollection
				.AddInfrastructure()
				.AddApplication();
		}
	}

}

