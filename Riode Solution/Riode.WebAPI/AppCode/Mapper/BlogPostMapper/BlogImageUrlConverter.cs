using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Core.Extensions;
using Riode.Data.Entities;
using Riode.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Mapper.BlogPostMapper
{
	public class BlogImageUrlConverter : IValueConverter<BlogPost, string>
	{
		public string Convert(BlogPost sourceMember, ResolutionContext context)
		{
			if (sourceMember == null)
			{
				return null;
			}

			var ctx = context.Items["ctx"] as IActionContextAccessor;

			if (ctx == null)
			{
				return sourceMember.ImagePath;
			}

			var domainName = ctx.GetAppLink();
			//return Path.Combine(domainName, "uploads", "images", "blog", sourceMember.ImagePath);
			return $"{domainName}/uploads/images/blog/{sourceMember.ImagePath}";
		}
	}
}
