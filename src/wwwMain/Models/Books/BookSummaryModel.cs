using System;
using System.Linq.Expressions;
using BookRepo.data.Entities;

namespace BookRepo.wwwMain.Models.Books {
	public class BookSummaryModel {
		public string Id { get; set; }
		public string Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }

		public static Expression<Func<Book, BookSummaryModel>> GetMap() =>
			x => new BookSummaryModel {
				Id = x.Id.ToString(),
				Isbn = x.Isbn,
				Title = x.Title,
				Author = x.Author
			};
	}
}
