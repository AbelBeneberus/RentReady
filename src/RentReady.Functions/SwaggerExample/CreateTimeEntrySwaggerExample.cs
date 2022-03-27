using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;

namespace RentReady.Functions.SwaggerExample
{
	[OpenApiExample(typeof(CreateTimeEntryExample))]
	public class CrateTimeEntryParameters
	{
		[OpenApiProperty()]
		[Newtonsoft.Json.JsonProperty("startOn", Required = Newtonsoft.Json.Required.Always)]
		[System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
		public string StartOn { get; set; }


		[OpenApiProperty()]
		[Newtonsoft.Json.JsonProperty("endOn", Required = Newtonsoft.Json.Required.Always)]
		[System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
		public string EndOn { get; set; }
	}
	public class CreateTimeEntryExample : OpenApiExample<CrateTimeEntryParameters>
	{
		public override IOpenApiExample<CrateTimeEntryParameters> Build(NamingStrategy namingStrategy = null)
		{
			this.Examples.Add(
			   OpenApiExampleResolver.Resolve(
				   "CreateTimeEntryExample",
				   new CrateTimeEntryParameters()
				   {
					   StartOn = DateTime.Now.ToShortDateString(),
					   EndOn = DateTime.Now.ToShortDateString()
				   },
				   namingStrategy
			   ));

			return this;
		}
	}
}
