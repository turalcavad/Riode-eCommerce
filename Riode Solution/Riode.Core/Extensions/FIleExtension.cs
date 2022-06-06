using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;



namespace Riode.Core.Extensions
{
	public static partial class Extension
	{
		public static async Task<string> SaveFile(this IHostingEnvironment env, IFormFile file, CancellationToken cancellationToken, string prefix = null)
		{
			string fileExtension = Path.GetExtension(file.FileName);

			string name = $"{(string.IsNullOrWhiteSpace(prefix) ? "" : prefix + "-")}{Guid.NewGuid()}{fileExtension}";

			string path = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "products", name);

			using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				await file.CopyToAsync(fs, cancellationToken);
			}

			return name;
		}
	}
}
