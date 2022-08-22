using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moss.Client;
using Moss.Report.Client;

namespace Moss.Tests
{
	[TestClass]
	public class IntegrationTest
	{
		[TestMethod]
		public async Task Test()
		{
			// arrange
			var client = new MossClient(987654321, new MossClientOptions() { Language = MossLanguage.Cpp });

			using var clientHandler = new HttpClientHandler();
			using var httpClient = new HttpClient(clientHandler);
			var reportClient = new MossReportClient(httpClient);

			client.AddBaseFile(Encoding.UTF8.GetBytes(BaseFiles.HelloWorld), "Base File Hello World");

			client.AddFile(Encoding.UTF8.GetBytes(Files.OurWorld), "student1/File Our World");
			client.AddFile(Encoding.UTF8.GetBytes(Files.ManyWorlds), "student2/File Many Worlds");

			// act
			var result = client.Submit();
			Debug.Print(result?.ToString());

			// assert
			Assert.IsNotNull(result);

			// act
			var report = await reportClient.GetReport(result).ConfigureAwait(false);

			// assert
			Assert.AreEqual(1, report.Length);
			Assert.AreEqual("student1/File_Our_World (90%)", report.First().FirstFile);
			Assert.AreEqual("student2/File_Many_Worlds (90%)", report.First().SecondFile);
			Assert.AreEqual(3, report.First().LinesMatched);
		}
	}
}