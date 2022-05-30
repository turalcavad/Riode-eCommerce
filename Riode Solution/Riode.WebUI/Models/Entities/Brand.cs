using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Brand : BaseEntity
	{
		
		[Required]
		public string Name { get; set; }
	}
}
