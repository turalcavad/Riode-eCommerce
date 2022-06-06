using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		readonly RiodeDbContext db;
		public CategoriesController(RiodeDbContext db)
		{
			this.db = db;
		}

		[Authorize(Policy = "admin.categories.index")]
		public IActionResult Index()
		{

			var model = db.Categories
				.Include(c => c.Parent)
				.ToList();
			return View(model);
		}
		public IActionResult Create()
		{
			var categories = db.Categories
					.Select(BindParentName)
					.ToList();

			ViewBag.Categories = new SelectList(categories, "Id", "Name", null, "ParentName");
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				var categories = db.Categories
					.Select(BindParentName)
					.ToList();

				ViewBag.Categories = new SelectList(categories, "Id", "Name", category.ParentId, "ParentName");
				

				db.Categories.Add(category);
				db.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(category);
		}

		[NonAction]
		public Category BindParentName(Category c) 
		{
			if (c.ParentId == null)
			{
				c.ParentName = "---";
			}
			else
			{
				c.ParentName = c.Parent.Name;
			}
			return c;
		} 

		public IActionResult Edit(int id)
		{
			var entity = db.Categories.FirstOrDefault(c => c.Id == id);
			ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", entity.Parent);
			if (entity == null)
				return NotFound();

			return View(entity);
		}
		[HttpPost]
		public IActionResult Edit([FromRoute] int id, Category category)
		{
			if (id != category.Id)
				return BadRequest();

			if (category.ParentId == category.Id)
			{
				ModelState.AddModelError("Parent Id", "Cannot be its parent");
				return View(category);
			}


			if (ModelState.IsValid)
			{
				db.Categories.Update(category);
				db.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return View(category);

		}

		public IActionResult Details(int id)
		{
			var entity = db.Categories
				.Include(c => c.Parent)
				.FirstOrDefault(c => c.Id == id);

			if (entity == null)
				return NotFound();

			return View(entity);
		}

		public IActionResult Delete([FromRoute] int id)
		{
			var entity = db.Categories.FirstOrDefault(c => c.Id == id);
			if (entity == null)
			{
				return Json(new
				{
					error = true,
					message = "It does not exist!"
				});
			}


			db.Categories.Remove(entity);
			db.SaveChanges();

			return Json(new
			{
				error = false,
				message = "Deleted!"
			});

		}
	}
}
