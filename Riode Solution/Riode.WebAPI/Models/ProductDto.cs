using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Mapper.ProductMapper
{
	public class ProductDto 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string StockKeepingUnit { get; set; }
		public int BrandId { get; set; }
		public string BrandName { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public int CreatedById { get; set; }
		public string CreatedDate { get; set; }
		public string CreatedByName { get; set; }
		public string MainImageUrl { get; set; }
		public IEnumerable<string> OtherImageUrls { get; set; }
	}
}
