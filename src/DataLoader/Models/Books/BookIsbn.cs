using System;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace DataLoader.Models.Books {
	public class BookIsbn {
		public string Isbn { get; set; }

		public static Expression<Func<Book, BookIsbn>> GetMap() =>
			x => new BookIsbn {
				Isbn = x.Isbn
			};

		public static Expression<Func<ExtnBookData, BookIsbn>> GetMapExtn() =>
			x => new BookIsbn {
				Isbn = x.Isbn
			};
	}
}
