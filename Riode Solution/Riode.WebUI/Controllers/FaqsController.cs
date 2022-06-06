using Microsoft.AspNetCore.Mvc;
using Riode.Data.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class FaqsController : Controller
	{
		readonly RiodeDbContext db;
		public FaqsController(RiodeDbContext db)
		{
			this.db = db;
		}
		public IActionResult Index()
		{
			var data = db.Faqs;
			return View(data);
		}
	}
}
