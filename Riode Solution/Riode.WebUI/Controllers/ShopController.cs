using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.Data.DataContexts;
using Riode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class ShopController : Controller
	{
		readonly RiodeDbContext db;
		public ShopController(RiodeDbContext db)
		{
			this.db = db;
		}
		public IActionResult Index()
		{
			var model = new ShopIndexViewModel();
			model.Brands = db.Brands.ToList();
			model.Colors = db.Colors.ToList();
			model.Sizes = db.Sizes.ToList();
			model.Categories = db.Categories
				.Include(c => c.Children)
				.ToList();
			return View(model);
		}
	}
}
