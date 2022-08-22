using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Moss.Client.Exceptions;

namespace Moss.Client
{
	public class MossClient
	{
		private readonly long _userId;
		private readonly MossClientOptions _options;
		private readonly List<(byte[] File, string DisplayName, string Language)> _baseFiles = new();
		private readonly List<(byte[] File, string DisplayName, string Language)> _files = new();


		/// <param name="userId">The userid is used to authenticate your queries to the server</param>
		/// <param name="options">Client options</param>
		public MossClient(long userId, MossClientOptions options)
		{
			_userId = userId;
			_options = options;
		}

		/// <param name="userId">The userid is used to authenticate your queries to the server</param>
		public MossClient(long userId) : this( userId, new MossClientOptions()) { }

		/// <summary>
		/// Adds base file. Moss normally reports all code that matches in pairs of files. When a base file is supplied, program code that also appears in the base file is not counted in matches. A typical base file will include, for example, the instructor-supplied  code for an assignment. You should  use a base file if it is convenient; base files improve results, but are not usually necessary for obtaining useful information. 
		/// </summary>
		/// <param name="fileBytes">File UTF-8 bytes</param>
		/// <param name="fileDisplayName">File display name in moss report. If you don't know what to specify, just pass the file path.</param>
		/// <param name="language">Programming language</param>
		/// <returns></returns>
		public MossClient AddBaseFile(byte[] fileBytes, string fileDisplayName, MossLanguage? language = null)
		{
			_baseFiles.Add((fileBytes, fileDisplayName, MossLanguageMap.Dictionary[language ?? _options.Language]));
			return this;
		}

		/// <summary>
		/// Adds file to compare/analyze.
		/// </summary>
		/// <param name="fileBytes">File UTF-8 bytes</param>
		/// <param name="fileDisplayName">File display name in moss report. If you don't know what to specify, just pass the file path.</param>
		/// <param name="language">Programming language</param>
		public MossClient AddFile(byte[] fileBytes, string fileDisplayName, MossLanguage? language = null)
		{
			_files.Add((fileBytes, fileDisplayName, MossLanguageMap.Dictionary[language ?? _options.Language]));
			return this;
		}

		/// <summary>
		/// Submits request and receives report url
		/// </summary>
		/// <returns>Report Url</returns>
		/// <exception cref="MossClientException">Thrown if the server did not return report Url</exception>
		public Uri Submit()
		{
			var ipe = new IPEndPoint(Dns.GetHostEntry(_options.Server).AddressList.First(), _options.Port);
			using var socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(ipe);

			SendOption(socket, new KeyValuePair<string, string>("moss", _userId.ToString(CultureInfo.InvariantCulture)));
			SendOption(socket, new KeyValuePair<string, string>("directory", _options.IsDirectoryMode ? "1" : "0"));
			SendOption(socket, new KeyValuePair<string, string>("X", _options.IsBetaRequest ? "1" : "0"));
			SendOption(socket, new KeyValuePair<string, string>("maxmatches", _options.MaxMatches.ToString(CultureInfo.InvariantCulture)));
			SendOption(socket, new KeyValuePair<string, string>("show", _options.NumberOfResultsToShow.ToString(CultureInfo.InvariantCulture)));

			var index = 0;
			_baseFiles.ForEach(f => SendFile(socket, index, f.File, f.Language, f.DisplayName));
			_files.ForEach(f => SendFile(socket, ++index, f.File, f.Language, f.DisplayName));

			SendOption(socket, new KeyValuePair<string, string>("query 0", _options.Comment.ToString(CultureInfo.InvariantCulture)));

			var bytes = new byte[1024];
			socket.Receive(bytes);

			SendOption(socket, new KeyValuePair<string, string>("end", string.Empty));

			return Uri.TryCreate(Encoding.UTF8.GetString(bytes).Trim('\0'), UriKind.Absolute, out var uri) ? uri : throw new MossClientException("Unable to get Url");
		}

		private void SendOption(Socket socket, KeyValuePair<string, string> pair)
		{
			socket.Send(Encoding.UTF8.GetBytes($"{pair.Key} {pair.Value}\n"));
		}

		private void SendFile(Socket socket, int number, byte[] file, string language, string displayName)
		{
			displayName = (_options.IsDirectoryMode ? displayName.Replace("\\", "/") : displayName).Replace(" ", "_");
			socket.Send(Encoding.UTF8.GetBytes($"file {number} {language} {file.Length} {displayName}\n"));
			socket.Send(file);
		}
	}
}