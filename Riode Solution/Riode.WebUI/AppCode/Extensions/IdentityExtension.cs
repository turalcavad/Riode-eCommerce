using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.Models.Entities.Membership;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{
		public static string GetPrincipalName(this ClaimsPrincipal principal)
		{
			string name = principal.Claims.FirstOrDefault(p => p.Type.Equals("name"))?.Value;
			string surname = principal.Claims.FirstOrDefault(p => p.Type.Equals("surname"))?.Value;


			if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname))
			{
				return $"{name} {surname}";
			}

			
			return principal.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Email))?.Value;
		}

		public static bool HasAccess(this ClaimsPrincipal user, string claim)
		{
			return user.IsInRole("SuperAdmin") || (user.HasClaim(c => c.Type.Equals(claim) && c.Value.Equals("1")));

		}

		public static int GetUserId(this ClaimsPrincipal user)
		{
			return Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
		}
	}
}
