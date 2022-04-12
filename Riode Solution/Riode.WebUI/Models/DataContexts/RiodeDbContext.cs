using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.DataContexts
{
	public class RiodeDbContext : DbContext
	{
		public RiodeDbContext(DbContextOptions options)
			:base(options)
		{

		}
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProductSize> Sizes { get; set; }
		public DbSet<ProductColor> Colors { get; set; }
		public DbSet<Faq> Faqs { get; set; }

		public DbSet<Blog> Blogs { get; set; }



	}
}
