namespace Riode.Data.Entities
{
	public class ProductSpecification
	{
		public int SpecificationId { get; set; }
		public virtual Specification Specification { get; set; }
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
		public string Value { get; set; }
	}
}
