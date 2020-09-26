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
				scraper.Reparse(raw.BookFinder);
			}

			if (raw.IsbnDb?.RawHtml != null) {
				scraper = ForIsbnDb();
				scraper.Reparse(raw.IsbnDb);
			}

			if (raw.OpenLibrary?.RawHtml != null) {
				scraper = ForOpenLibrary();
				scraper.Reparse(raw.OpenLibrary);
			}
		}

		public static async Task<Book> CreateBook(ExtnBookData raw) {
			var retval = new Book { Isbn = raw.Isbn, CreatedOn = DateTime.UtcNow };
			retval.Title = raw.OpenLibrary?.Title ?? raw.BookFinder?.Title ?? raw.IsbnDb?.Title;
			retval.Author = raw.OpenLibrary?.Author ?? raw.BookFinder?.Author ?? raw.IsbnDb?.Author;
			retval.Publisher = raw.OpenLibrary?.Publisher ?? raw.BookFinder?.Publisher ?? raw.IsbnDb?.Publisher;
			retval.Description = raw.OpenLibrary?.Description ?? raw.BookFinder?.Description ?? raw.IsbnDb?.Description;
			retval.PublishedOnRaw = raw.OpenLibrary?.DatePublished ?? raw.BookFinder?.DatePublished ?? raw.IsbnDb?.DatePublished;
			retval.NumPages = raw.OpenLibrary?.NumPages ?? raw.BookFinder?.NumPages ?? raw.IsbnDb?.NumPages;
			if (DateTime.TryParse(retval.PublishedOnRaw, out var dt)) retval.PublishedOn = dt;
			var coverUrl = raw.OpenLibrary?.CoverImageUrl ?? raw.BookFinder?.CoverImageUrl ?? raw.IsbnDb?.CoverImageUrl;
			if (coverUrl != null) {
				using var http = new HttpClient();
				var msg = await http.GetAsync(coverUrl);
				if (msg.IsSuccessStatusCode) {
					var img = await msg.Content.ReadAsByteArrayAsync();
					if (img.Length > 0) {
						retval.Cover = new List<byte>(img);
					}
				}
			}
			return retval;
		}
	}
}
