using System;
using BookRepo.data.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRepo.data.Entities {
	[CollectionName("aaaSystemConfig")]
	[BsonIgnoreExtraElements(true)]
	public class SystemConfig : EntityBase {
		public string SchemaVersion { get; set; }
	}
}
