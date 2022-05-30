using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; }
		public int? ParentId { get; set; }
		public virtual Category Parent { get; set; }
		public virtual ICollection<Category> Children { get; set; }

		[NotMapped]
		public string ParentName { get; set; }

		public virtual ICollection<BlogPost> BlogPosts { get; set; }
	}
}
