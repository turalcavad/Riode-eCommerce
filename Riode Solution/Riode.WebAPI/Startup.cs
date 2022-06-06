using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Riode.Core.Providers;
using Riode.Data.DataContexts;
using Riode.Data.Entities.Membership;
using Riode.WebAPI.AppCode.Configuration;
using Riode.WebAPI.AppCode.Mapper.BrandMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riode.WebAPI
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(cfg =>
			{
				var policy = new AuthorizationPolicyBuilder()
								.RequireAuthenticatedUser()
								.Build();
				cfg.Filters.Add(new AuthorizeFilter(policy));
			})
				.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
				

			InjectDependecies.Setup(services);

			services.AddAutoMapper(typeof(Startup).Assembly);

			services.AddDbContext<RiodeDbContext>(cfg =>
			{
				cfg.UseSqlServer(configuration.GetConnectionString("cString"));
			});

			MembershipDependecies.Setup(services);


			services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
			services.AddScoped<IClaimsTransformation, AppClaimProvider>();


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


			SwaggerDependecies.Setup(services);

			services.AddAuthentication(cfg=> {
				cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						LifetimeValidator = Validate,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["jwt:issuer"],
						ValidAudience = configuration["jwt:audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:saltKey"]))
					};
				});


					services.AddAuthorization(cfg=> {
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
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseStaticFiles();

			SwaggerDependecies.Apply(app);

			app.UseAuthentication();
			app.UseAuthorization();



			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		bool Validate(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			if (DateTime.UtcNow <= expires)
			{
				return true;
			}

			return false;
		}
	}


}
