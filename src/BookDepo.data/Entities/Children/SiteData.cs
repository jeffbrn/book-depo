using System;

namespace BookRepo.data.Entities.Children {
	public class SiteData {
		public string Title { get; set; }
		public string Author { get; set; }
		public string DatePublished { get; set; }
		public int? NumPages { get; set; }
		public string Description { get; set; }
		public string CoverImageUrl { get; set; }
		public string RawHtml { get; set; }
	}
}
