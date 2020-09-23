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

/*
 * Linqpad code

//const string url = "https://openlibrary.org/search?isbn=9780765388919";
//var web = new HtmlWeb();
var doc = new HtmlDocument();
doc.Load(@"C:\temp\HeadOn_OpenLibrary.html");

var node = doc.QuerySelectorAll("h1[itemprop=name]").FirstOrDefault();
///Title
(node?.FirstChild.InnerText ?? "Unknown").Dump();

node = doc.QuerySelectorAll("img.cover").FirstOrDefault();
///Cover image source
node?.Attributes["src"]?.Value?.Dump();

node = doc.QuerySelectorAll("a[itemprop=author]").FirstOrDefault();
///Author
(node?.FirstChild?.InnerText ?? "Unknown").Dump();

node = doc.QuerySelectorAll("strong[itemprop=datePublished]").FirstOrDefault();
///Date published
(node?.FirstChild?.InnerText ?? "Unknown").Dump();

node = doc.QuerySelectorAll("a[itemprop=publisher]").FirstOrDefault();
///Publisher
(node?.FirstChild?.InnerText ?? "Unknown").Dump();

node = doc.QuerySelectorAll("span[itemprop=numberOfPages]").FirstOrDefault();
///#Pages
(node?.FirstChild?.InnerText ?? "Unknown").Dump();

node = doc.QuerySelectorAll("div.book-description-content > p").FirstOrDefault();
///Description
(node?.FirstChild?.InnerText ?? "Unknown").Dump();


 */
