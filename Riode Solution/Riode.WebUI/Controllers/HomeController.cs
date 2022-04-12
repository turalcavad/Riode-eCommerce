using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class HomeController : Controller
	{
		readonly RiodeDbContext db;

		public HomeController(RiodeDbContext db)
		{
			this.db = db;

		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		
	}
}
