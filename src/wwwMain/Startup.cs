using System;
using BookRepo.wwwMain.Services.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;

namespace BookRepo.wwwMain {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.Config(Configuration);

			services.AddControllersWithViews();

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration => {
				configuration.RootPath = "wwwroot";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					"default",
					"{controller}/{action=Index}/{id?}");
				endpoints.MapToVueCliProxy(
					"{*path}",
					new SpaOptions { SourcePath = "clientapp" },
					npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
					regex: "App running at:",
					forceKill: true
				);
			});
		}
	}
}
