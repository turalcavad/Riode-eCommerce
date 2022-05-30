using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.FormModels
{
	public class RegisterFormModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		//[Required]
		//public string UserName { get; set; }
		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
