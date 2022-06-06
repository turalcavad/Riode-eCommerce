using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Riode.Core.Extensions
{
	public static partial class Extension
	{
		public static string GetAppLink(this IActionContextAccessor ctx)
		{
			string scheme = ctx.ActionContext.HttpContext.Request.Scheme;
			string host = ctx.ActionContext.HttpContext.Request.Host.ToString();

			return $"{scheme}://{host}";
		}
		
		public static HttpRequest CopyTo(this HttpRequest request, string key, IDictionary<string,object> destination)
		{
			if (request.Headers.TryGetValue(key, out StringValues values))
			{

				destination.Add(key, values.First());
			}

			return request;
		}
	}
}
