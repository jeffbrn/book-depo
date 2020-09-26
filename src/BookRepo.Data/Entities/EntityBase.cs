using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRepo.Data.Entities {
	[BsonIgnoreExtraElements(true)]
	public class EntityBase {
		public ObjectId Id { get; set; }
	}
}
