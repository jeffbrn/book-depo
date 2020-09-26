using System;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace BookRepo.wwwMain.Models.Books {
	public class BookImageModel {
		public byte[] Cover { get; set; }

		public static Expression<Func<Book, BookImageModel>> GetMap() =>
			x => new BookImageModel {
				Cover = x.Cover.ToArray()
			};
	}
}
