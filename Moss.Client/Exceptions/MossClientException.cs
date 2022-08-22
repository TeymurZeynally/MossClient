using System;

namespace Moss.Client.Exceptions
{
	internal class MossClientException : Exception
	{
		public MossClientException(string message): base(message)
		{
		}
	}
}
