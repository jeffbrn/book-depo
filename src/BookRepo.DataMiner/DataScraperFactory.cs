using System;
using BookRepo.DataMiner.BookFinder;
using BookRepo.DataMiner.ISBNdb;
using BookRepo.DataMiner.OpenLibrary;

namespace BookRepo.DataMiner {
	public static class DataScraperFactory {
		public static IDataScraper ForBookFinder() => new ScraperBookFinder();

		public static IDataScraper ForIsbnDb() => new ScraperIsbn();

		public static IDataScraper ForOpenLibrary() => new ScraperOpenLib();
	}
}
