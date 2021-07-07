using System;
using System.Collections.Generic;
using BookRepo.Data.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRepo.Data.Entities {
	[CollectionName("books")]
	[BsonIgnoreExtraElements(true)]
	public class Book : EntityBase {
		public DateTime CreatedOn { get; init; }
		public string Isbn { get; init; } = "";
		public string Title { get; init; } = "";
		public string? Author { get; init; }
		public string? PublishedOnRaw { get; init; }
		public DateTime? PublishedOn { get; init; }
		public string? Publisher { get; init; }
		public int? NumPages { get; init; }
		public string? Description { get; init; }
		public List<string> Tags { get; init; } = new();
		public List<byte>? Cover { get; init; }
	}
}
