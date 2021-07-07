using System;

namespace BookRepo.Data.Entities.Children {
	public class SiteData {
		public string Title { get; init; } = "";
		public string? Author { get; init; }
		public string? Publisher { get; init; }
		public string? DatePublished { get; init; }
		public int? NumPages { get; init; }
		public string? Description { get; init; }
		public string? CoverImageUrl { get; init; }
		public string RawHtml { get; init; } = "";
	}
}
