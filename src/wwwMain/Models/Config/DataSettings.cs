using System;
using BookRepo.Data.Common;

namespace BookRepo.wwwMain.Models.Config {
	public class DataSettings {
		public MongoConnectionSettings Mongo { get; set; }
	}
}
