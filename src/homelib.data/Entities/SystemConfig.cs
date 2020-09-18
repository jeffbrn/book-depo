using System;
using homelib.data.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace homelib.data.Entities {
	[CollectionName("aaaSystemConfig")]
	[BsonIgnoreExtraElements(true)]
	public class SystemConfig : EntityBase {
		public string SchemaVersion { get; set; }
	}
}
