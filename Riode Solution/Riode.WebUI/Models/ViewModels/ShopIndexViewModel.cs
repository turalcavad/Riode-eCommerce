using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Data.ViewModels
{
	public class ShopIndexViewModel
	{
		public List<Brand> Brands { get; set; }
		public List<ProductColor> Colors { get; set; }
		public List<ProductSize> Sizes { get; set; }
		public List<Category> Categories { get; set; }
	}
}
