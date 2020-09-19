using System;
using System.Text;
using DataLoader.Models.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BookRepo.data.Common;

namespace DataLoader.Application {
	public class DataLoaderApp : ConsoleAppBase {
		/// <inheritdoc />
		public DataLoaderApp(string[] args) : base(args) { }

		protected override void RegisterServices(IServiceCollection services) {
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddScoped(c => c.GetService<IOptions<AppSettings>>().Value.Services.Data.Mongo.GetConnection());
		}

		protected override void OnBeforeRun() {
			base.OnBeforeRun();
			var config = RootScope.ServiceProvider.GetService<IOptions<AppSettings>>().Value;
			var log = RootScope.ServiceProvider.GetService<ILogger<DataLoaderApp>>();
			var configMsg = new StringBuilder();
			configMsg.AppendLine("**********************************************************************************************************");
			configMsg.AppendLine($"* Runtime Environment = {RuntimeEnvironment}");
			configMsg.AppendLine("* ");
			configMsg.AppendLine("* Configuration:");
			var mongoConfig = config.Services.Data.Mongo;
			configMsg.AppendLine($"*     Mongo Connection: mongodb://{mongoConfig.ServerAddr}/{mongoConfig.DatabaseName}");
			configMsg.AppendLine($"*      Data Source Directory: {config.DataSrcDir}");
			configMsg.AppendLine("* ");
			configMsg.AppendLine("**********************************************************************************************************");
			log.LogInformation(configMsg.ToString());
		}
	}
}
