using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentReady.Application.Exceptions;

namespace RentReady.Functions.Helpers
{
	public class HttpFunctionExecutor : IHttpFunctionExecutor
	{
		private readonly ILogger<HttpFunctionExecutor> _logger;
		public HttpFunctionExecutor(ILogger<HttpFunctionExecutor> logger)
		{
			_logger = logger; 
		}
		public async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func)
		{
			try
			{
				return await func();
			}
			catch (ValidationException ex)
			{
				var result = new
				{
					message = "Validation failed.",
					errors = ex.Errors.Select(x => new
					{
						x.PropertyName,
						x.ErrorMessage,
						x.ErrorCode
					})
				};
				_logger.LogError("Validation Failed", ex);
				return new BadRequestObjectResult(result);
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
