using System;
using System.Threading.Tasks;
using BookRepo.data.Entities.Children;

namespace BookRepo.DataMiner {
	public interface IDataScraper {
		Task<SiteData> GetData(string bookIsbn);
	}
}
