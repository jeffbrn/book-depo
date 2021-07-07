using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookRepo.Data.Entities;

namespace BookRepo.Data.Models {
	public class LibraryStatsModel {
		public long NumBooks { get; init; }
		public List<BookInfoModel> LatestUploaded { get; init; } = new();
	}

	public class BookInfoModel {
		public string Id { get; init; } = "";
		public string Title { get; init; } = "";
		public DateTime AddedOn { get; init; }

		public static Expression<Func<Book, BookInfoModel>> GetMap() =>
			x => new BookInfoModel {Id = x.Id.ToString(), Title = x.Title, AddedOn = x.CreatedOn};
	}
}
