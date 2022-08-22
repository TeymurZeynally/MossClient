namespace Moss.Client
{
	public class MossClientOptions
	{
		public string Server { get; init; } = "moss.stanford.edu";

		public int Port { get; init; } = 7690;

		public MossLanguage Language { get; init; } = MossLanguage.C;

		public string Comment { get; init; } = string.Empty;

		public int MaxMatches { get; init; } = 10;

		public int NumberOfResultsToShow { get; init; } = 250;

		public bool IsDirectoryMode { get; init; }

		public bool IsBetaRequest { get; init; }
	}
}
