using System;
using BookRepo.Data.Common;
using BookRepo.Data.Entities.Children;

namespace BookRepo.Data.Entities {
	[CollectionName("raw-data")]
	public class ExtnBookData : EntityBase {
		public string Isbn { get; init; } = "";
		public DateTime ImportedOn { get; init; }
		public SiteData? BookFinder { get; set; }
		public SiteData? IsbnDb { get; set; }
		public SiteData? OpenLibrary { get; set; }
	}
}
