using System;

namespace BookRepo.DataMiner.BookFinder {
	public class ScraperBookFinder : BaseDataScraper {
		#region Overrides of BaseDataScraper

		/// <inheritdoc />
		protected override string BookDataUrl { get; } = "https://www.bookfinder.com/search/?author=&title=&lang=en&isbn={ISBN}&new_used=*&destination=us&currency=USD&mode=basic&st=sr&ac=qr";

		/// <inheritdoc />
		protected override string TitleQuery { get; } = "#describe-isbn-title|.text";

		/// <inheritdoc />
		protected override string AuthorQuery { get; } = "span[itemprop=author]|.text";

		/// <inheritdoc />
		protected override string DatePublishedQuery { get; } = null;

		/// <inheritdoc />
		protected override string NumPagesQuery { get; } = null;

		/// <inheritdoc />
		protected override string DescriptionQuery { get; } = "#bookSummary > p|.text[]";

		/// <inheritdoc />
		protected override string CoverImgQuery { get; } = "img#coverImage|src";

		#endregion
	}
}
