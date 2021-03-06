﻿using System;
using System.Threading.Tasks;
using BookRepo.Data.Entities.Children;

namespace BookRepo.DataMiner {
	public interface IDataScraper {
		Task<SiteData> GetData(string bookIsbn);

		void Reparse(SiteData data);
	}
}
