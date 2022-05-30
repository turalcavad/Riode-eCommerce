using Microsoft.AspNetCore.Mvc;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class ContactController : Controller
	{
		readonly RiodeDbContext db;
		public ContactController(RiodeDbContext db)
		{
			this.db = db;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult PostComment(ContactComment cm)
		{
			if (cm == null)
			{
				return Json(new
				{
					error = true,
					message = "Something went wrong, please try again!"
				});

			}

			if (ModelState.IsValid)
			{
				db.ContactComments.Add(cm);
				db.SaveChanges();

			}

			return Json(new
			{
				error = false,
				message = "Success!"
			});
			
		}

	}
}
