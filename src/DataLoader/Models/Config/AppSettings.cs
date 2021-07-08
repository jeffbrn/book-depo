using System;

namespace DataLoader.Models.Config {
	public class AppSettings {
		public ServiceSettings Services { get; init; } = new();
		public string DataSrcDir { get; init; } = "";
	}
}
