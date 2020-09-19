using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookRepo.data.Entities {
	[BsonIgnoreExtraElements(true)]
	public class EntityBase {
		public ObjectId Id { get; set; }
	}
}
