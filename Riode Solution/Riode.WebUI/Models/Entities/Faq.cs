﻿using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Models.Entities
{
	public class Faq : BaseEntity
	{
		
		public string Question { get; set; }
		public string Answer { get; set; }


	}
}
