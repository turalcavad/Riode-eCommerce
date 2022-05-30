using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class PostTag : BaseEntity
	{
		public string Name { get; set; }
		public virtual ICollection<BlogPostTag> Tags { get; set; }
	}

	public class BlogPostTag
	{
		public int BlogPostId { get; set; }
		public virtual BlogPost BlogPost { get; set; }
		public int PostTagId { get; set; }
		public virtual PostTag PostTag { get; set; }
	}
}
