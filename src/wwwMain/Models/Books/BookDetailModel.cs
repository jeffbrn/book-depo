using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace BookRepo.wwwMain.Models.Books {
	public class BookDetailModel {
		public string Id { get; set; }
		public string Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string PublishedOnRaw { get; set; }
		public DateTime? PublishedOn { get; set; }
		public string Publisher { get; set; }
		public int? NumPages { get; set; }
		public string Description { get; set; }
		public List<string> Tags { get; set; } = new List<string>();

		public static Expression<Func<Book, BookDetailModel>> GetMap() =>
			x => new BookDetailModel {
				Id = x.Id.ToString(),
				Isbn = x.Isbn,
				Title = x.Title,
				Author = x.Author,
				PublishedOnRaw = x.PublishedOnRaw,
				PublishedOn = x.PublishedOn,
				Publisher = x.Publisher,
				NumPages = x.NumPages,
				Description = x.Description,
				Tags = x.Tags
			};
	}
}
