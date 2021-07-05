using System;

namespace BookRepo.WebApi.Models.Config {
	public class ServiceSettings {
		public DataSettings Data { get; init; } = new();
	}
}
