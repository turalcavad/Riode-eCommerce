using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.Core.Extensions;
using Riode.Core.Infrastructure;
using Riode.Business.Modules.ProductModule;
using Riode.Data.DataContexts;
using Riode.Data.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly RiodeDbContext _context;
        readonly IMediator mediator;

        public ProductsController(RiodeDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var riodeDbContext = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsMain == true));
            return View(await riodeDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["Colors"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["Sizes"] = new SelectList(_context.Sizes, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.Select(BindParentName).ToList(), "Id", "Name", null, "ParentName");
            ViewBag.Specifications = _context.Specifications.Where(s => s.DeletedById == null).ToList();

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            var response = await mediator.Send(command);

            if (response?.ValidationResult!=null && !response.ValidationResult.IsValid)
            {
                return Json(response.ValidationResult);
            }

            return Json(new CommandJsonResponse(false, "Success"));
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Specifications)

                .Include(p => p.Pricing)
                .ThenInclude(p => p.Color)

                .Include(p => p.Pricing)
                .ThenInclude(p => p.Size)

                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories.Select(BindParentName).ToList(), "Id", "Name", product.CategoryId, "ParentName");
            ViewData["Colors"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["Sizes"] = new SelectList(_context.Sizes, "Id", "Name");
            ViewBag.Specifications = _context.Specifications.Where(s => s.DeletedById == null).ToList();

            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditCommand command)
        {
            var response = await mediator.Send(command);

            if (response?.ValidationResult != null && !response.ValidationResult.IsValid)
            {
                return Json(response.ValidationResult);
            }

            return Json(new CommandJsonResponse(false, "Success"));
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
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

    }
}
