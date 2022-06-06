using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Core.Extensions;
using Riode.Data.Entities;
using System.Collections.Generic;

namespace Riode.WebAPI.AppCode.Mapper.ProductMapper
{
	public class ProductResolver : IValueResolver<Product, ProductDto, IEnumerable<string>>
	{
		public IEnumerable<string> Resolve(Product source, ProductDto destination, IEnumerable<string> destMember, ResolutionContext context)
		{
			if (source.Images != null)
			{
				var ctx = context.Items["ctx"] as IActionContextAccessor;

				string domainName = "";

				if (ctx != null)
				{
					domainName = ctx.GetAppLink();	
				}


				foreach (var item in source.Images)
				{
					if (item.IsMain)
					{
						destination.MainImageUrl = $"{domainName}/uploads/images/products/{item.ImagePath}";
					}
					else
					{ 
						yield return $"{domainName}/uploads/images/products/{item.ImagePath}";
					}
				}
			}

			
		}
	}
}
