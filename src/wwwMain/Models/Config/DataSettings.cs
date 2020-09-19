using System;
using BookRepo.data.Common;

namespace BookRepo.wwwMain.Models.Config {
	public class DataSettings {
		public MongoConnectionSettings Mongo { get; set; }
	}
}
