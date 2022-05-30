﻿using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string StockKeepingUnit { get; set; }
		public int BrandId { get; set; }
		public virtual Brand Brand { get; set;}
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public virtual ICollection<ProductImage> Images { get; set; }
		public virtual ICollection<ProductSpecification> Specifications { get; set; }
		public virtual ICollection<ProductPricing> Pricing { get; set; }



	}
}
