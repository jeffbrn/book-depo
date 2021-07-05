using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace BookRepo.WebApi.Models.Books {
	public class BookDetailModel {
		public string Id { get; private init; } = "";
		public string Isbn { get; private init; } = "";
		public string Title { get; private init; } = "";
		public string? Author { get; private init; }
		public string? PublishedOnRaw { get; private init; }
		public DateTime? PublishedOn { get; private init; }
		public string? Publisher { get; private init; }
		public int? NumPages { get; private init; }
		public string? Description { get; private init; }
		public List<string> Tags { get; private init; } = new List<string>();

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
