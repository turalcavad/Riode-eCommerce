using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Modules.ProductModule
{
	public class ProductPricing
	{
		public int ProductId { get; set; }
		public int ColorId { get; set; }
		public int SizeId { get; set; }
		public double Price { get; set; }
	}

}
