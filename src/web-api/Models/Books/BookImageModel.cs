using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace BookRepo.WebApi.Models.Books {
	public class BookImageModel {
		public List<byte>? Cover { get; private init; }

		public static Expression<Func<Book, BookImageModel>> GetMap() =>
			x => new BookImageModel {
				Cover = x.Cover
			};
	}
}
