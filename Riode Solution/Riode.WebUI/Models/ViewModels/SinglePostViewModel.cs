using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Data.ViewModels
{
	public class SinglePostViewModel
	{
		public BlogPost Post { get; set; }

		public IEnumerable<BlogPost> RelatedPosts { get; set; }
	}
}
