using System.ComponentModel.DataAnnotations;

namespace Riode.Data.Entities
{
	public class Brand : BaseEntity
	{
		
		[Required]
		public string Name { get; set; }
	}
}
