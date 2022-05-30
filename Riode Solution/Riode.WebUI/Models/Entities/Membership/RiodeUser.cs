using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities.Membership
{
	public class RiodeUser : IdentityUser<int>
	{
		[Required]
		public string Name { get; set; }
		
		[Required]
		public string Surname { get; set; }
	}
}
