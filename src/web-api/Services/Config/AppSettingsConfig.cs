using System;
using BookRepo.Data.Common;
using BookRepo.Data.Repository;
using BookRepo.Data.Repository.Impl;
using BookRepo.WebApi.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookRepo.WebApi.Services.Config {
	public static class AppSettingsConfig {
		public static AppSettings Config(this IServiceCollection services, IConfiguration config) {
			services.AddOptions();
			
			var section = config.GetSection("AppSettings") ?? throw new InvalidOperationException("Missing or invalid configuration");
			services.Configure<AppSettings>(section);

			services.AddScoped(c => c.GetService<IOptions<AppSettings>>()?.Value.Services.Data.Mongo.GetConnection() ?? throw new InvalidOperationException("Application not configured correctly"));
			services.AddScoped<IBookRepo, MongoBookRepoImpl>();


			using var sp = services.BuildServiceProvider();
			var opts = sp.GetRequiredService<IOptions<AppSettings>>();
			return opts.Value;
		}
	}
}
