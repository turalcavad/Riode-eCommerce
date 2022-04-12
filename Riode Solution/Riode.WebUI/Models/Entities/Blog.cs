using Riode.WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Blog
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string PhotoPath { get; set; }



	}
}
