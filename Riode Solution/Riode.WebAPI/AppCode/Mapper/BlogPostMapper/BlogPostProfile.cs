using AutoMapper;
using Riode.Core.Extensions;
using Riode.Core.Infrastructure;
using Riode.Data.Entities;
using Riode.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Mapper.BlogPostMapper
{
	public class BlogPostProfile : Profile
	{
		public BlogPostProfile()
		{
			CreateMap<BlogPost, BlogPostSingleDto>();

			CreateMap<BlogPost, BlogPostSingleDto>()
				.ForMember(dest => dest.Body, src => src.MapFrom(s => s.Body.HtmlToPlainText()))
				.ForMember(dest => dest.ImageUrl, src => src.ConvertUsing(new BlogImageUrlConverter(), src => src))
				.ForMember(dest => dest.AcceptedDate, src => src.ConvertUsing(new DateTimeConverter(), src => src.CreatedDate));

			CreateMap<BlogPost, BlogPostDto>()
				.ForMember(dest=> dest.Body, src=> src.MapFrom(s=> s.Body.HtmlToPlainText().ToEllipse(50)))
				.ForMember(dest=> dest.ImageUrl, src=> src.ConvertUsing(new BlogImageUrlConverter(), src=> src))
				.ForMember(dest => dest.AcceptedDate, src => src.ConvertUsing(new DateTimeConverter(), src => src.CreatedDate));
			
			CreateMap<PagedViewModel<BlogPost>, PagedViewModel<BlogPostDto>>();
		}
	}
}
