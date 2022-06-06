using System.Collections.Generic;

namespace Riode.Data.Entities
{
	public class Specification : BaseEntity
	{
		public string Name { get; set; }
		public virtual ICollection<ProductSpecification> Specifications { get; set; }
	}
}
