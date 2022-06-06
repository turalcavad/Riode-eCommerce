using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Riode.Core.Providers;
using Riode.Data.DataContexts;
using Riode.Data.Entities.Membership;
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

			//to require authorization
			services.AddControllersWithViews(cfg => {

				cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

				var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();

				cfg.Filters.Add(new AuthorizeFilter(policy));
			});


			services.AddRouting(cfg => cfg.LowercaseUrls = true);

			services.AddDbContext<RiodeDbContext>(cfg => {
				cfg.UseSqlServer(configuration.GetConnectionString("cString"));

			});

			services.AddIdentity<RiodeUser, RiodeRole>()
				.AddEntityFrameworkStores<RiodeDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(cfg =>
			{
				cfg.Password.RequireDigit = false;
				cfg.Password.RequireLowercase = false;
				cfg.Password.RequireUppercase = false;
				cfg.Password.RequireNonAlphanumeric = false;
				cfg.Password.RequiredLength = 3;

				cfg.User.RequireUniqueEmail = true;
				cfg.Lockout.MaxFailedAccessAttempts = 3;
				cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 3, 0);
			});

			services.ConfigureApplicationCookie(cfg => {
				cfg.LoginPath = "/signin.html";
				cfg.AccessDeniedPath = "/accessdenied.html";
				cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
				cfg.Cookie.Name = "Riode";
			});

			services.AddAuthentication();
			services.AddAuthorization(cfg=> 
			{
			foreach (var policyName in AppClaimProvider.principals)
				{
					cfg.AddPolicy(policyName, p =>
					{
						p.RequireAssertion(handler =>
						{
							return handler.User.IsInRole("SuperAdmin") || handler.User.HasClaim(policyName, "1");
						});
					});
				}
			}); //superadmin Authorization

			services.AddScoped<UserManager<RiodeUser>>();
			services.AddScoped<SignInManager<RiodeUser>>();


			var assemblies = AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => a.FullName.StartsWith("Riode.")).ToArray();
			services.AddMediatR(assemblies);


			services.AddFluentValidation(cfg =>
			{
				cfg.RegisterValidatorsFromAssemblies(assemblies);
			});


			services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

			services.AddScoped<IClaimsTransformation, AppClaimProvider>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseStaticFiles();
			app.UseEndpoints(cfg => 
			{
				cfg.MapControllerRoute(
					name: "default-accessdenied",
					pattern: "accessdenied.html",
					defaults: new
					{
						area = "",
						controller = "account",
						action = "accessdenied"
					});


				cfg.MapAreaControllerRoute(
					name: "defaultAdmin",
					areaName: "Admin",
					pattern: "admin/{controller=dashboard}/{action=index}/{id?}");

				cfg.MapControllerRoute("default", pattern: "{controller=home}/{action=index}/{id?}");

			});
		}
	}
}
