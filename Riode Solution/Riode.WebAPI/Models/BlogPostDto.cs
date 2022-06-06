using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.Models
{
	public class BlogPostDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public string ImageUrl { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int CreatedById  { get; set; }
		public string AcceptedDate  { get; set; }
		public string CreatedByName  { get; set; }
	}

	public class BlogPostSingleDto : BlogPostDto
	{ 
	}
}
