﻿namespace Riode.Data.Entities
{
	public class ProductImage : BaseEntity
	{
		public string ImagePath { get; set; }
		public bool IsMain { get; set; }
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
