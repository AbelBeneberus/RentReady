using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public string OrderNumber { get; set; }
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
				   StartOn = DateTime.Now.AddHours(-1).ToString(),
				   OrderNumber = DateTime.Now.ToString()
			   },
			   namingStrategy
		   )) ;

			return this;
		}
	}
}
