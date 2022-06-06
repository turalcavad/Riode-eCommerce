using AutoMapper;
using Riode.Core.Infrastructure;
using Riode.Data.Entities;
using Riode.WebAPI.AppCode.Mapper.BlogPostMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Mapper.ProductMapper
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.CreatedDate, src => src.ConvertUsing(new DateTimeConverter(), m => m.CreatedDate))
				.ForMember(dest => dest.OtherImageUrls, src => src.MapFrom<ProductResolver>());

			CreateMap<PagedViewModel<Product>, PagedViewModel<ProductDto>>();
		}
	}
}
