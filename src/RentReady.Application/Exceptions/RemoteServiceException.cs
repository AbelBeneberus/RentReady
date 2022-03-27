namespace RentReady.Application.Exceptions
{
	public class RemoteServiceException : Exception
	{
		public RemoteServiceException(Exception ex): base(ex.Message, ex)
		{
			 
		}
	}
}
