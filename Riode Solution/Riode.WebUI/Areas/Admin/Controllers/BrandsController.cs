using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riode.Business.Modules.BrandModule;
using System.Threading.Tasks;

namespace Riode.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class BrandsController : Controller
    {

        readonly IMediator mediator;

        public BrandsController( IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: Admin/Brands

        [Authorize(Policy = "admin.brands.index")]
        public async Task<IActionResult> Index()
        {
           
            var data = await mediator.Send(new BrandsAllQuery());

            return View(data);
        }

        // GET: Admin/Brands/Details/5
        [Authorize(Policy = "admin.brands.details")]
        public async Task<IActionResult> Details(BrandSingleQuery query)
        {
            var entity = await mediator.Send(query);

            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [Authorize(Policy = "admin.brands.create")]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Policy = "admin.brands.create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                var response =  await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }


        [Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(BrandSingleQuery query)
        {
            var brand = await mediator.Send(query);

            var command = new BrandEditCommand();

            command.Id = brand.Id;
            command.Name = brand.Name;

            if (brand == null)
            {
                return NotFound();
            }
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(int id, BrandEditCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);

                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Policy = "admin.brands.delete")]
        public async Task<IActionResult> Delete(BrandRemoveCommand command)
        {
            var response = await mediator.Send(command);

            return Json(response);

        }
      
    }
}
