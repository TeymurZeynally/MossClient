namespace Moss.Report.Client.Exceptions
{
	public class MossReportClientException : Exception
	{
		public MossReportClientException()
		{
		}

		public MossReportClientException(string message) : base(message)
		{
		}

		public MossReportClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
