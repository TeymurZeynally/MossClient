using HtmlAgilityPack;

namespace Moss.Report.Client
{
	internal class MossReportExtractor
	{
		public MossReportRow[] ExtractReport(string html)
		{
			var document = new HtmlDocument();
			document.LoadHtml(html);
			return document.DocumentNode
				.SelectNodes("//table").Single()
				.SelectNodes("tr").Skip(1)
				.Select(cells => new MossReportRow
				{
					FirstFile = cells.ChildNodes[0].InnerText.Trim(),
					SecondFile = cells.ChildNodes[1].InnerText.Trim(),
					LinesMatched = long.Parse(cells.ChildNodes[2].InnerText)
				}).ToArray();
		}
	}
}
