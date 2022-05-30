using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SpecificationsController : Controller
	{
		readonly RiodeDbContext db;
		public SpecificationsController(RiodeDbContext db)
		{
			this.db = db;
		}
		public async Task<IActionResult> Index()
		{
			var model =  await db.Specifications
				.Where(s=> s.DeletedById == null)
				.ToListAsync();
			return View(model);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Specification specification)
		{
			if (ModelState.IsValid)
			{
				db.Specifications.Add(specification);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(specification);
		}

		public async Task<IActionResult> Edit([FromRoute] int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var entity = await db.Specifications.FirstOrDefaultAsync(s => s.Id == id);

			if (entity == null)
				return NotFound();

			return View(entity);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, Specification specification)
		{
			if (id != specification.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					db.Update(specification);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SpecificationExists(specification.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(specification);
		}

		public IActionResult Details([FromRoute] int id)
		{
			var entity = db.Specifications.FirstOrDefault(s => s.Id == id);

			if (entity == null)
				return NotFound();

			return View(entity);
		}

		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var entity = await db.Specifications.FirstOrDefaultAsync(c => c.Id == id);
			if (entity == null)
			{
				return Json(new
				{
					error = true,
					message = "It does not exist!"
				});

			}
			entity.DeletedById = 1;
			entity.DeletedDate = DateTime.UtcNow.AddHours(4);
			db.SaveChanges();

			return Json(new
			{
				error = false,
				message = "Deleted"
			});
		}


		private bool SpecificationExists(int id)
		{
			return db.Specifications.Any(e => e.Id == id);
		}


	}
}
