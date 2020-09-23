using System;

namespace BookRepo.DataMiner.ISBNdb {
	public class ScraperIsbn : BaseDataScraper {
		#region Overrides of BaseDataScraper

		/// <inheritdoc />
		protected override string BookDataUrl { get; } = "https://isbndb.com/book/{ISBN}";

		/// <inheritdoc />
		protected override string TitleQuery { get; } = "div#block-multipurpose-business-theme-page-title > h1|.text";

		/// <inheritdoc />
		protected override string AuthorQuery { get; } = null;

		/// <inheritdoc />
		protected override string DatePublishedQuery { get; } = null;

		/// <inheritdoc />
		protected override string NumPagesQuery { get; } = null;

		/// <inheritdoc />
		protected override string DescriptionQuery { get; } = null;

		/// <inheritdoc />
		protected override string CoverImgQuery { get; } = "div.artwork > object|.data";

		#endregion
	}
}
