using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Configuration
{
	internal static class SwaggerDependecies
	{
		internal static IServiceCollection Setup(this IServiceCollection services) 
		{
			services.AddSwaggerGen(option=> {
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
				});

			option.AddSecurityRequirement(new OpenApiSecurityRequirement
			 {
				 {
					  new OpenApiSecurityScheme
					 {
							 Reference = new OpenApiReference
							 {
								 Type = ReferenceType.SecurityScheme,
								 Id = "Bearer"
							 }
						 },
						 new string[] {}
				 }
			 });
			});
			return services;
		}
		
		internal static IApplicationBuilder Apply(this IApplicationBuilder app) 
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
