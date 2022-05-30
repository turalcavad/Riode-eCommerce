using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class BlogPost : BaseEntity
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public string ImagePath { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public virtual ICollection<BlogPostTag> Tags { get; set; }


	}
}
