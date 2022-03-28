using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using RentReady.Application.Commands.CreateTimeEntry;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using RentReady.Functions.SwaggerExample;
using RentReady.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace RentReady.Functions.Functions
{
	public class CreateTimeEntryFunction
	{
				private readonly IMediator _mediator;
		private readonly IHttpFunctionExecutor _httpFunctionExecutor;

		public CreateTimeEntryFunction(IMediator mediator, IHttpFunctionExecutor httpFunctionExecutor)
		{
		
			_mediator = mediator;
			_httpFunctionExecutor = httpFunctionExecutor;
		}

		[FunctionName("CreateTimeEntry")]
		[OpenApiOperation(operationId: "Run", tags: new[] { "command" })]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CrateTimeEntryParameters), Description = "CrateTimeEntryParametersExample", Required = true)]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post", "get", Route = null)] CreateTimeEntryCommand command,
			ILogger log)
		{
			try
			{
				return await _httpFunctionExecutor.ExecuteAsync(async () =>
							{
								var result = await _mediator.Send(command);

								return new OkObjectResult(new { Message = "Time Entry Record created for the this days succesfuly.", Dates = result.Dates });
							});
			}
			catch (RemoteServiceException ex)
			{
				var result = new ObjectResult(ex.Message);
				result.StatusCode = StatusCodes.Status503ServiceUnavailable;
				return result;
			}
			catch (System.Exception ex)
			{
				var result = new ObjectResult(ex.Message);
				result.StatusCode = StatusCodes.Status500InternalServerError;
				return result;
			}
		}

	}
}
