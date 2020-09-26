using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BookRepo.Data.Common;
using BookRepo.Data.Entities;
using BookRepo.Data.Entities.Children;
using BookRepo.Data.Repository;
using BookRepo.DataMiner;
using DataLoader.Models.Books;
using DataLoader.Models.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataLoader.Application {
	public class Startup : IStartup {
		private readonly ILogger _log;
		private readonly AppSettings _settings;
		private readonly IBookRepo _repo;

		public Startup(ILogger<Startup> log, IOptions<AppSettings> config, IMongoDatabase db, IBookRepo repo) {
			_log = log;
			_settings = config.Value;
			db.CheckSchema();
			_repo = repo;
		}

		#region Implementation of IStartup

		/// <inheritdoc />
		public void Run(CancellationToken cancel) {
			//var wait = Task.Delay(10000, cancel);
			//wait.Wait(cancel);
			Task.Run(async () => {
				var dataDir = new DirectoryInfo(_settings.DataSrcDir);
				if (!dataDir.Exists) {
					throw new ApplicationException($"Data directory {_settings.DataSrcDir} does not exist");
				}

				var loadedRaw = new List<ExtnBookData>();
				foreach (var isbnDir in dataDir.EnumerateDirectories()) {
					_log.LogInformation("Directory found: {0}", isbnDir.Name);
					// check to see if book already exists
					var raw = await _repo.GetRawData(isbnDir.Name);
					if (raw != null) continue;
					_log.LogInformation("New book data, parsing and loading: {0}", isbnDir.Name);
					raw = await LoadBook(isbnDir);
					loadedRaw.Add(raw);
				}

				foreach (var rawBook in loadedRaw) {
					var chk = await _repo.GetOne(rawBook.Isbn, BookIsbn.GetMap());
					if (chk != null) continue;
					_log.LogInformation("Parsing raw data to load book {0}", rawBook.Isbn);
					var bk = await DataScraperFactory.CreateBook(rawBook);
					await _repo.Insert(bk);
				}
			}, cancel).Wait(cancel);

			_log.LogWarning("Task finished.");
		}

		#endregion

		private async Task<ExtnBookData> LoadBook(DirectoryInfo bookDir) {
			var raw = new ExtnBookData { Isbn = bookDir.Name, ImportedOn = DateTime.UtcNow };
			var opt = new EnumerationOptions {
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseInsensitive,
				MatchType = MatchType.Simple,
				RecurseSubdirectories = false,
				ReturnSpecialDirectories = false
			};
			foreach (var dataFile in bookDir.EnumerateFiles("*.raw", opt)) {
				using var dataStr = dataFile.OpenText();
				var htmlRaw = await dataStr.ReadToEndAsync();
				switch (dataFile.Name.ToLowerInvariant()) {
					case "bookfinder.raw":
						raw.BookFinder = new SiteData {RawHtml = htmlRaw};
						break;
					case "isbndb.raw":
						raw.IsbnDb = new SiteData { RawHtml = htmlRaw };
						break;
					case "openlibrary.raw":
						raw.OpenLibrary = new SiteData { RawHtml = htmlRaw };
						break;
				}
			}

			DataScraperFactory.ReparseBookData(raw);
			await _repo.StoreRawData(raw);
			return raw;
		}

	}
}
