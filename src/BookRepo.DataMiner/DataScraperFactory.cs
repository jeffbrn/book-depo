using System;
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
	}
}
