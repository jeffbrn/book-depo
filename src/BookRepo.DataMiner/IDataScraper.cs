using System;
using System.Threading.Tasks;

namespace BookRepo.DataMiner {
	public interface IDataScraper {
		Task<BookData> GetData(string bookIsbn);
	}
}
