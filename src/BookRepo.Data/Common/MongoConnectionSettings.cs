using System;

namespace BookRepo.Data.Common {
	public class MongoConnectionSettings {
		public string ServerAddr { get; init; } = "";
		public string DatabaseName { get; init; } = "";
	}
}
