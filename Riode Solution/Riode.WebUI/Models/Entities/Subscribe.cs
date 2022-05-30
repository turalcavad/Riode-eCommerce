using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Subscribe : BaseEntity
	{
		public string Email { get; set; }
		public bool? EmailConfirmed { get; set; }
		public bool EmailSent { get; set; } = false;
		public DateTime? EmailConfirmedDate { get; set; } = DateTime.UtcNow.AddHours(4);
		public DateTime? AppliedDate { get; set; }

	}
}
