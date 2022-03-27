using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentReady.Application.Exceptions
{
	public class ServiceIsNotReadyException : Exception
	{
		public ServiceIsNotReadyException(Exception exception)
			: base(exception.Message, exception)
		{

		}
	}
}
