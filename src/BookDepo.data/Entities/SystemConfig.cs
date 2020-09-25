using System;
using BookRepo.Data.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRepo.Data.Entities {
	[CollectionName("aaaSystemConfig")]
	[BsonIgnoreExtraElements(true)]
	public class SystemConfig : EntityBase {
		public string SchemaVersion { get; set; }
	}
}
