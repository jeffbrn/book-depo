using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookRepo.Data.Entities;
using BookRepo.DataMiner.BookFinder;
using BookRepo.DataMiner.ISBNdb;
using BookRepo.DataMiner.OpenLibrary;

namespace BookRepo.DataMiner {
	public static class DataScraperFactory {
		public static IDataScraper ForBookFinder() => new ScraperBookFinder();

		public static IDataScraper ForIsbnDb() => new ScraperIsbn();

		public static IDataScraper ForOpenLibrary() => new ScraperOpenLib();

		public static void ReparseBookData(ExtnBookData raw) {
			IDataScraper scraper;

			if (raw.BookFinder?.RawHtml != null) {
				scraper = ForBookFinder();
				raw.BookFinder = scraper.Reparse(raw.BookFinder.RawHtml);
			}

			if (raw.IsbnDb?.RawHtml != null) {
				scraper = ForIsbnDb();
				raw.IsbnDb = scraper.Reparse(raw.IsbnDb.RawHtml);
			}

			if (raw.OpenLibrary?.RawHtml != null) {
				scraper = ForOpenLibrary();
				raw.OpenLibrary = scraper.Reparse(raw.OpenLibrary.RawHtml);
			}
		}

		public static async Task<Book> CreateBook(ExtnBookData raw) {
			var pubOnRaw = raw.OpenLibrary?.DatePublished ?? raw.BookFinder?.DatePublished ?? raw.IsbnDb?.DatePublished;
			var pubOn = DateTime.TryParse(pubOnRaw, out var dt) ? dt : (DateTime?)null;

			List<byte>? cover = null;
			var coverUrl = raw.OpenLibrary?.CoverImageUrl ?? raw.BookFinder?.CoverImageUrl ?? raw.IsbnDb?.CoverImageUrl;
			if (coverUrl != null) {
				using var http = new HttpClient();
				var msg = await http.GetAsync(coverUrl);
				if (msg.IsSuccessStatusCode) {
					var img = await msg.Content.ReadAsByteArrayAsync();
					if (img.Length > 0) {
						cover = new List<byte>(img);
					}
				}
			}

			var retval = new Book {
				Isbn = raw.Isbn,
				CreatedOn = DateTime.UtcNow,
				Title = raw.OpenLibrary?.Title ?? raw.BookFinder?.Title ?? raw.IsbnDb?.Title ?? "Unknown",
				Author = raw.OpenLibrary?.Author ?? raw.BookFinder?.Author ?? raw.IsbnDb?.Author,
				Publisher = raw.OpenLibrary?.Publisher ?? raw.BookFinder?.Publisher ?? raw.IsbnDb?.Publisher,
				Description = raw.OpenLibrary?.Description ?? raw.BookFinder?.Description ?? raw.IsbnDb?.Description,
				PublishedOnRaw = pubOnRaw,
				NumPages = raw.OpenLibrary?.NumPages ?? raw.BookFinder?.NumPages ?? raw.IsbnDb?.NumPages,
				PublishedOn = pubOn,
				Cover = cover
			};

			return retval;
		}
	}
}
