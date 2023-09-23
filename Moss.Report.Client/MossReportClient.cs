using Moss.Report.Client.Exceptions;

namespace Moss.Report.Client
{
	public class MossReportClient
	{
		private readonly HttpClient _httpClient;
		private readonly MossReportExtractor _mossReportExtractor;

		public MossReportClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_mossReportExtractor = new MossReportExtractor();
		}

		/// <summary>
		/// Requests server and extracts report table content
		/// </summary>
		/// <param name="reportUri">Stanford moss result url</param>
		/// <exception cref="MossReportClientException">Thrown if there is problem with communication with server</exception>
		/// <returns></returns>
		public async Task<MossReportRow[]> GetReport(Uri reportUri)
		{
			try
			{
				var response = await _httpClient.GetAsync(reportUri).ConfigureAwait(false);
				var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				return _mossReportExtractor.ExtractReport(content);
			}
			catch (HttpRequestException exception)
			{
				throw new MossReportClientException("Unable to get moss report", exception);
			}
		}
	}
}