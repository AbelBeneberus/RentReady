using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentReady.Application.Exceptions
{
	public class UnableToInizializeServiceClient: Exception
	{
		public UnableToInizializeServiceClient(Exception exception): base(exception.Message, exception)
		{

		}
	}
}
