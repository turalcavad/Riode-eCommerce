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
		DbSet<Brand> Brands { get; set; }
	}
}
