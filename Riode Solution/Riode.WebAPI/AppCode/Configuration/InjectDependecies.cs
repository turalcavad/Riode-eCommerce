using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Configuration
{
	internal static class InjectDependecies
	{
		internal static IServiceCollection Setup(this IServiceCollection services)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => a.FullName.StartsWith("Riode."))
				.ToArray();

			services.AddMediatR(assemblies);

			services.AddFluentValidation(cfg =>
			{
				cfg.RegisterValidatorsFromAssemblies(assemblies);
			});

			return services;

		}
	}
}
