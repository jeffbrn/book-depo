using System;
using BookRepo.data.Common;
using BookRepo.data.Repository;
using BookRepo.data.Repository.Impl;
using BookRepo.wwwMain.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookRepo.wwwMain.Services.Config {
	public static class AppSettingsConfig {
		public static void Config(this IServiceCollection services, IConfiguration config) {
			services.Configure<AppSettings>(config.GetSection("AppSettings"));
			services.AddScoped(c => c.GetService<IOptions<AppSettings>>().Value.Services.Data.Mongo.GetConnection());
			services.AddScoped<IBookRepo, MongoBookRepoImpl>();
		}
	}
}
