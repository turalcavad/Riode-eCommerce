using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Modules.SubcsribeModule;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class HomeController : Controller
	{
		readonly RiodeDbContext db;
		readonly IMediator mediator;

		public HomeController(RiodeDbContext db, IMediator mediator)
		{
			this.db = db;
			this.mediator = mediator;

		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}

		//public IActionResult Subscribe(Subscribe s)
		//{
		//	var current = db.Subscribes.FirstOrDefault(e => e.Email.Equals(s));

		//	if (current != null && current.EmailConfirmed == true)
		//	{
		//		return Json(new
		//		{

		//			error = true,
		//			message = "You already subscribed"
		//		});
		//	}

		//	if (ModelState.IsValid)
		//	{
		//		db.Subscribes.Add(s);
		//		db.SaveChanges();

		//		return Json(new
		//		{
		//			error = false,
		//			message = "Please check your email for confirmation!"
		//		});

		//	}

		//	return Json(new
		//	{
		//		error = true,
		//		message = "Something went wrong, please try again!"

		//	});
			
		//}

		[HttpPost]
		public async Task<IActionResult> Subscribe(SubscribeCreateCommand command)
		{
			var response = await mediator.Send(command);

			return Json(response);
		}

		[HttpGet]
		[Route("/subscribe-confirm")]
		public async Task<IActionResult> SubscribeConfirm(SubscribeConfirmCommand command)
		{
			var response = await mediator.Send(command);

			return View(response);

		}
	}
}
