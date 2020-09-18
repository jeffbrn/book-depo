using System;

namespace homelib.data.Common {
	[AttributeUsage(AttributeTargets.Class)]
	public class CollectionNameAttribute : Attribute {
		public CollectionNameAttribute(string collectionName) {
			CollectionName = collectionName;
		}

		public string CollectionName { get; }
	}
}
