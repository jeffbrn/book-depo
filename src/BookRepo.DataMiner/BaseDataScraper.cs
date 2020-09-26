using System;
using System.Linq;
using System.Threading.Tasks;
using BookRepo.Data.Entities.Children;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace BookRepo.DataMiner {
	public abstract class BaseDataScraper : IDataScraper {
		private HtmlDocument _html;

		/// <inheritdoc />
		public async Task<SiteData> GetData(string bookIsbn) {
			var url = BookDataUrl.Replace("{ISBN}", bookIsbn);
			var web = new HtmlWeb();
			_html = await web.LoadFromWebAsync(url);
			var retval = new SiteData { RawHtml = _html.ParsedText };
			ParseHtml(retval);
			return retval;
		}

		/// <inheritdoc />
		public void Reparse(SiteData data) {
			_html = new HtmlDocument();
			_html.LoadHtml(data.RawHtml);
			ParseHtml(data);
		}

		protected abstract string BookDataUrl { get; } 

		protected abstract string TitleQuery { get; }

		protected abstract string AuthorQuery { get; }

		protected abstract string Publisher { get; }

		protected abstract string DatePublishedQuery { get; }

		protected abstract string NumPagesQuery { get; }

		protected abstract string DescriptionQuery { get; }

		protected abstract string CoverImgQuery { get; }

		private void ParseHtml(SiteData data) {
			data.Title = QueryDoc(TitleQuery);
			data.Author = QueryDoc(AuthorQuery);
			data.Publisher = QueryDoc(Publisher);
			data.DatePublished = QueryDoc(DatePublishedQuery);
			var np = QueryDoc(NumPagesQuery);
			if (np != null & int.TryParse(np, out var tmpint)) data.NumPages = tmpint;
			data.Description = QueryDoc(DescriptionQuery);
			data.CoverImageUrl = QueryDoc(CoverImgQuery);
			if (string.IsNullOrWhiteSpace(data.CoverImageUrl)) data.CoverImageUrl = null;
			if (data.CoverImageUrl != null && data.CoverImageUrl.StartsWith("//")) data.CoverImageUrl = $"https:{data.CoverImageUrl}";
		}

		private string QueryDoc(string query) {
			if (query == null) return null;
			var parse = query.Split('|');
			var selector = parse[0];
			var multi = parse[1].EndsWith("[]");
			var attrib = multi ? parse[1][0..^2] : parse[1];
			if (attrib.Equals(".text")) attrib = null;
			return multi ? QueryDocMultiNode(selector, attrib) : QueryDocSingleNode(selector, attrib);
		}

		/// <summary>
		/// Queries the html for a single text value
		/// </summary>
		/// <param name="query">Css selector query</param>
		/// <param name="attribName">if the result is in an attribute then this is the name, if null return the inner text of the node</param>
		/// <returns></returns>
		private string QueryDocSingleNode(string query, string attribName) {
			var node = _html.QuerySelectorAll(query).FirstOrDefault();
			if (node == null) return null;
			return (attribName == null) ? node.FirstChild?.InnerText : node.Attributes[attribName]?.Value;
		}

		private string QueryDocMultiNode(string query, string attribName) {
			var nodes = _html.QuerySelectorAll(query);
			if ((nodes?.Count ?? 0) == 0) return null;
			var vals = nodes.Select(n => attribName == null ? n.InnerText ?? "" : n.Attributes[attribName]?.Value ?? "").Select(t => t.Trim());
			var retval = string.Join(" ", vals).Trim();
			return retval.Length == 0 ? null : retval;
		}
	}
}
