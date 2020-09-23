using System;
using BookRepo.data.Common;
using BookRepo.data.Entities.Children;

namespace BookRepo.data.Entities {
	[CollectionName("raw-data")]
	public class ExtnBookData : EntityBase {
		public string Isbn { get; set; }
		public DateTime ImportedOn { get; set; }
		public SiteData BookFinder { get; set; }
		public SiteData IsbnDb { get; set; }
		public SiteData OpenLibrary { get; set; }
	}
}
