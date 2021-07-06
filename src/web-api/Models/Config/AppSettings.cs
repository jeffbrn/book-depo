using System;

namespace BookRepo.WebApi.Models.Config {
	public class AppSettings {
		public SecuritySettings Security { get; init; } = new();
		public ServiceSettings Services { get; init; } = new();
	}
}
