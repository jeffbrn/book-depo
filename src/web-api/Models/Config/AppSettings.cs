using System;

namespace BookRepo.WebApi.Models.Config {
	public class AppSettings {
		public ServiceSettings Services { get; init; } = new();
	}
}
