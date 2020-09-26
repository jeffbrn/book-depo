using System;

namespace BookRepo.DataMiner.OpenLibrary {
	public class ScraperOpenLib : BaseDataScraper {

		#region Overrides of BaseDataScraper

		/// <inheritdoc />
		protected override string BookDataUrl { get; } = "https://openlibrary.org/search?isbn={ISBN}";

		/// <inheritdoc />
		protected override string TitleQuery { get; } = "h1[itemprop=name]|.text";

		/// <inheritdoc />
		protected override string AuthorQuery { get; } = "a[itemprop=author]|.text";

		/// <inheritdoc />
		protected override string Publisher { get; } = "a[itemprop=publisher]|.text";

		/// <inheritdoc />
		protected override string DatePublishedQuery { get; } = "strong[itemprop=datePublished]|.text";

		/// <inheritdoc />
		protected override string NumPagesQuery { get; } = "span[itemprop=numberOfPages]|.text";

		/// <inheritdoc />
		protected override string DescriptionQuery { get; } = "div.book-description-content > p|.text[]";

		/// <inheritdoc />
		protected override string CoverImgQuery { get; } = "img.cover|src";

		#endregion
	}
}
