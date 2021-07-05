using System;
using BookRepo.Data.Common;

namespace BookRepo.WebApi.Models.Config {
	public class DataSettings {
		public MongoConnectionSettings Mongo { get; init; } = new();
	}
}
