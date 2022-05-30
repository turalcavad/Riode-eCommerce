using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class ProductSize : BaseEntity
	{
		public string ShortName { get; set; }
		
		public string Name { get; set; }
	}
}
