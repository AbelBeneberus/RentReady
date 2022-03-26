using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using RentReady.Application.Commands.CreateTimeEntry;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using RentReady.Functions.SwaggerExample;

namespace RentReady.Functions.Functions
{
	public class CreateTimeEntryFunction
	{
		private readonly ILogger<CreateTimeEntryFunction> _logger;
		private readonly IMediator _mediator;
		private readonly IHttpFunctionExecutor _httpFunctionExecutor;

		public CreateTimeEntryFunction(ILogger<CreateTimeEntryFunction> logger, IMediator mediator, IHttpFunctionExecutor httpFunctionExecutor)
		{
			_logger = logger;
			_mediator = mediator;
			_httpFunctionExecutor = httpFunctionExecutor;
		}

		[FunctionName("CreateTimeEntry")]
		[OpenApiOperation(operationId: "Run", tags: new[] { "command" })]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CrateTimeEntryParameters), Description = "CrateTimeEntryParametersExample", Required = true)]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post", "get", Route = null)]
		CreateTimeEntryCommand command,
			ILogger log)
		{
			try
			{
				return await _httpFunctionExecutor.ExecuteAsync(async () =>
							{
								Unit result = await _mediator.Send(command);
								return new OkObjectResult(result);
							});
			}
			catch (System.Exception ex)
			{
				return new ObjectResult(ex.Message);
			}

		}

	}
}
