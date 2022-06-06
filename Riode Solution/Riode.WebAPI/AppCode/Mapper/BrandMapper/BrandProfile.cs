using AutoMapper;
using Riode.Data.Entities;
using Riode.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Mapper.BrandMapper
{
	public class BrandProfile : Profile
	{
		public BrandProfile()
		{
			CreateMap<Brand, BrandDto>()
				.ForMember(dest => dest.AuthorId, src => src.MapFrom(s => s.CreatedById))
				.ForMember(dest => dest.AuthorName, src => src.MapFrom(s => s.CreatedBy.Email));
		}
	}
}
