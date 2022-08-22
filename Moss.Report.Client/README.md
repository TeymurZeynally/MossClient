# What is Moss
Moss (for a Measure Of Software Similarity) is an automatic system for determining the similarity of programs. To date, the main application of Moss has been in detecting plagiarism in programming classes. Since its development in 1994, Moss has been very effective in this role. The algorithm behind moss is a significant improvement over other cheating detection algorithms.

For more information: http://theory.stanford.edu/~aiken/moss/

# Moss.Report.Client
Moss.Report.Client is a library for downloading and parsing a report with analysis results. The client downloads the report from the link, parses it and presents it as a structure.

## Installation 

```shell
Install-Package Moss.Report.Client
```

## Usage 

```csharp
using var clientHandler = new HttpClientHandler();
using var httpClient = new HttpClient(clientHandler);
var reportClient = new MossReportClient(httpClient);

var report = await reportClient.GetReport("http://moss.stanford.edu/results/...").ConfigureAwait(false);
```
