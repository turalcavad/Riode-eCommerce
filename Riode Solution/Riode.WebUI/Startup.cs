using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Riode.WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI
{
	public class Startup
	{

		readonly IConfiguration configuration;
		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddDbContext<RiodeDbContext>(cfg => {
				cfg.UseSqlServer(configuration.GetConnectionString("cString"));
			
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseRouting();
			app.UseStaticFiles();
			app.UseEndpoints(cfg => 
			{
				cfg.MapControllerRoute("default", pattern: "{controller=home}/{action=index}/{id?}");
				configuration.GetConnectionString("cString");

				cfg.MapAreaControllerRoute(
					name: "default",
					areaName: "Admin",
					pattern: "{controller=dashboard}/{action=index}/{id?}");
			});

			
			
		}
	}
}
