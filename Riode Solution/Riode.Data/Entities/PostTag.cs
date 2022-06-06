using System.Collections.Generic;

namespace Riode.Data.Entities
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
