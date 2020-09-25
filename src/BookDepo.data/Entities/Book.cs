using System;
using System.Collections.Generic;
using BookRepo.Data.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRepo.Data.Entities {
	[CollectionName("books")]
	[BsonIgnoreExtraElements(true)]
	public class Book : EntityBase {
		public DateTime CreatedOn { get; set; }
		public string Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string PublishedOnRaw { get; set; }
		public DateTime? PublishedOn { get; set; }
		public string Publisher { get; set; }
		public int? NumPages { get; set; }
		public string Description { get; set; }
		public List<string> Tags { get; set; } = new List<string>();
		public List<byte> Cover { get; set; }
	}
}
