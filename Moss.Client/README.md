# What is Moss
Moss (for a Measure Of Software Similarity) is an automatic system for determining the similarity of programs. To date, the main application of Moss has been in detecting plagiarism in programming classes. Since its development in 1994, Moss has been very effective in this role. The algorithm behind moss is a significant improvement over other cheating detection algorithms.

For more information: http://theory.stanford.edu/~aiken/moss/

# Moss.Client
Moss.Client is a library for interacting with the Moss server. The library provides the functionality of uploading files to the Moss server and getting a link to a report with the results.

## Installation 

```shell
Install-Package Moss.Client
```

## Usage 

```csharp
var userId = 987654321;
var client = new MossClient(userId, new MossClientOptions() { Language = MossLanguage.Cpp });

client.AddBaseFile(File.ReadAllBytes("/path/to/basefile.cpp"), fileDisplayName: "/path/to/basefile.cpp");

client.AddFile(File.ReadAllBytes("/path/to/file1.cpp"), fileDisplayName: "/path/to/file1.cpp");
client.AddFile(File.ReadAllBytes("/path/to/file2.cpp"), fileDisplayName: "/path/to/file2.cpp");

var result = client.Submit();
```