using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentReady.Infrastructure
{
	public class DataVerseConfiguration
	{
		public string ClientId { get; set; } = string.Empty; 
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string ApplicationUrl { get; set; } = string.Empty;
		public string RedirectUrl { get; set; } = string.Empty;
		public string ApiVersion { get; set; } = string.Empty;
		public string ConnectionString { get; set; } = string.Empty;
	}
}
