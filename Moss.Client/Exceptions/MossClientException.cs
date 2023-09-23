using System;

namespace Moss.Client.Exceptions
{
	public class MossClientException : Exception
	{
		public MossClientException()
		{
		}

		public MossClientException(string message): base(message)
		{
		}

		public MossClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
