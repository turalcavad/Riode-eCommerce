using Riode.Data.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Data.Entities
{
	public class BaseEntity : HistoryEntity
	{
		public int Id { get; set; }
		
	}

	public class HistoryEntity
	{
		public int? CreatedById { get; set; }
		public RiodeUser CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
		public int? DeletedById { get; set; }
		public RiodeUser DeletedBy { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
