using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.Models
{
	public class BrandDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? AuthorId { get; set; }
		public string AuthorName { get; set; }

	}
}
