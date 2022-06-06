using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Riode.Data.DataContexts;
using Riode.Data.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Configuration
{
	internal static class MembershipDependecies
	{
		internal static IServiceCollection Setup(this IServiceCollection services) 
		{
			services.AddIdentity<RiodeUser, RiodeRole>()
				.AddEntityFrameworkStores<RiodeDbContext>();

			services.AddScoped<SignInManager<RiodeUser>>();
			services.AddScoped<UserManager<RiodeUser>>();
			services.AddScoped<RoleManager<RiodeRole>>();
			return services;
		}
	}
}
