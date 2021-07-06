using System;

namespace BookRepo.WebApi.Models.Config {
	public class SecuritySettings {
		public string ClientAddress { get; init; } = "";
		public string ApiAddress { get; init; } = "";
	}
}
