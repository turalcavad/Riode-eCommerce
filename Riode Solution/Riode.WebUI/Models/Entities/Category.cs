using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Category
	{
		[Required]
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ParentId { get; set; }
		public virtual Category Parent { get; set; }
		public virtual ICollection<Category> Children { get; set; }
	}
}
