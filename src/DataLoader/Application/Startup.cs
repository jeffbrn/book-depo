using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using DataLoader.Models.Config;
using homelib.data.Common;
using homelib.data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace DataLoader.Application {
	public class Startup : IStartup {
		private readonly IMongoCollection<Book> _bookColl;
		private readonly ILogger _log;
		private readonly AppSettings _settings;

		public Startup(ILogger<Startup> log, IOptions<AppSettings> config, IMongoDatabase db) {
			_log = log;
			_settings = config.Value;
			db.CheckSchema();
			_bookColl = db.GetEntityCollection<Book>();
		}

		#region Implementation of IStartup

		/// <inheritdoc />
		public void Run(CancellationToken cancel) {
			//var wait = Task.Delay(10000, cancel);
			//wait.Wait(cancel);
			var dataDir = new DirectoryInfo(_settings.DataSrcDir);
			if (!dataDir.Exists) {
				throw new ApplicationException($"Data directory {_settings.DataSrcDir} does not exist");
			}

			foreach (var isbnDir in dataDir.EnumerateDirectories()) {
				_log.LogInformation("Directory found: {0}", isbnDir.Name);
				// check to see if book already exists
				if (_bookColl.Find(x => x.Isbn.Equals(isbnDir.Name)).Project(x => x.Isbn).FirstOrDefault() != null) continue;
				var book = LoadBook(isbnDir);
				_bookColl.InsertOne(book);
			}

			_log.LogWarning("Task finished.");
		}

		#endregion

		private Book LoadBook(DirectoryInfo bookDir) {
			var retval = new Book {Isbn = bookDir.Name};
			var cover = bookDir.EnumerateFiles().FirstOrDefault(f =>
				f.Extension.ToLowerInvariant().Equals(".jpg") || f.Extension.ToLowerInvariant().Equals(".jepg"));
			if (cover != null) {
				retval.Cover = new List<byte>(File.ReadAllBytes(cover.FullName));
			}

			var opt = new EnumerationOptions {
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseInsensitive,
				MatchType = MatchType.Simple,
				RecurseSubdirectories = false,
				ReturnSpecialDirectories = false
			};
			foreach (var dataFile in bookDir.EnumerateFiles("data*.json", opt)) {
				using var dataStr = dataFile.OpenText();
				var jsonRaw = dataStr.ReadToEnd();
				ReadJson(jsonRaw, retval);
			}

			return retval;
		}

		private void ReadJson(string rawJson, Book result) {
			var json = JObject.Parse(rawJson);
			result.Title ??= ReadValue(json, "details", "title", "value");
			result.Author ??= ReadValue(json, "details", "author", "value");
			if (result.PublishedOnRaw == null) {
				result.PublishedOnRaw = ReadValue(json, "details", "publishInfo", "date", "value");
				if (result.PublishedOnRaw != null) {
					result.PublishedOn ??= DateTime.TryParse(result.PublishedOnRaw, out var dt) ? dt : (DateTime?)null;
				}
			}

			result.Publisher ??= ReadValue(json, "details", "publishInfo", "publisher", "value");
			result.NumPages ??= Convert.ToInt32(ReadValue(json, "details", "numPages", "value"));
			result.Description ??= ReadValue(json, "details", "description", "value");
		}

		private string ReadValue(JToken json, params string[] path) {
			if (json == null) {
				return null;
			}

			if (path.Length == 0) {
				return null;
			}

			var nxtJson = json[path[0]];
			if (path.Length == 1) {
				return (string)nxtJson;
			}

			var nxtPath = path.Skip(1).ToArray();
			return ReadValue(nxtJson, nxtPath);
		}
	}
}
