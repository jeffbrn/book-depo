using System;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace BookRepo.WebApi.Models.Books {
	public class BookSummaryModel {
		public string Id { get; private init; } = "";
		public string Isbn { get; private init; } = "";
		public string Title { get; private init; } = "";
		public string? Author { get; private init; }

		public static Expression<Func<Book, BookSummaryModel>> GetMap() =>
			x => new BookSummaryModel {
				Id = x.Id.ToString(),
				Isbn = x.Isbn,
				Title = x.Title,
				Author = x.Author
			};
	}
}
