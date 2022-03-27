using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace RentReady.Functions.Helpers
{
	public class HttpFunctionExecutor : IHttpFunctionExecutor
	{
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
				
				return new BadRequestObjectResult(result);
			}
			 
		}
	}
}
