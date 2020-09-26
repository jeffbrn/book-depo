using System;
using BookRepo.Data.Common;

namespace DataLoader.Models.Config {
	public class DataSettings {
		public MongoConnectionSettings Mongo { get; set; }
	}
}
