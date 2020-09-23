using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace BookRepo.DataMiner {
	public abstract class BaseDataScraper : IDataScraper {
		private HtmlDocument _html;

		public async Task<BookData> GetData(string bookIsbn) {
			var url = BookDataUrl.Replace("{ISBN}", bookIsbn);
			var web = new HtmlWeb();
			_html = await web.LoadFromWebAsync(url);
			var retval = new BookData { RawHtml = _html.ParsedText };
			retval.Title = QueryDoc(TitleQuery);
			retval.Author = QueryDoc(AuthorQuery);
			retval.DatePublished = QueryDoc(DatePublishedQuery);
			var np = QueryDoc(NumPagesQuery);
			if (np != null & int.TryParse(np, out var tmpint)) retval.NumPages = tmpint;
			retval.Description = QueryDoc(DescriptionQuery);
			retval.CoverImageUrl = QueryDoc(CoverImgQuery);
			return retval;
		}

		protected abstract string BookDataUrl { get; } 

		protected abstract string TitleQuery { get; }

		protected abstract string AuthorQuery { get; }

		protected abstract string DatePublishedQuery { get; }

		protected abstract string NumPagesQuery { get; }

		protected abstract string DescriptionQuery { get; }

		protected abstract string CoverImgQuery { get; }

		private string QueryDoc(string query) {
			if (query == null) return null;
			var parse = query.Split('|');
			var selector = parse[0];
			var multi = parse[1].EndsWith("[]");
			var attrib = multi ? parse[1].Substring(0, parse[1].Length - 2) : parse[1];
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
			return retval.Equals("") ? null : retval;
		}
	}
}
