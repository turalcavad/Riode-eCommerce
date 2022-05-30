using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.AppCode.Modules.BlogPostModule;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly RiodeDbContext db;
        readonly IWebHostEnvironment env;
        IMediator mediator;

        public BlogPostsController(RiodeDbContext db, IWebHostEnvironment env, IMediator mediator)
        {
            this.db = db;
            this.env = env;
            this.mediator = mediator;
        }

        // GET: Admin/BlogPosts
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var query = db.BlogPosts.Include(b => b.Category)
                .Where(b=> b.DeletedById == null);
            var pagedModel = new PagedViewModel<BlogPost>(query, pageIndex, pageSize);
            return View(pagedModel);
        }

        // GET: Admin/BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Create
        public IActionResult Create()
        {
            var categories = db.Categories
                    .Select(BindParentName)
                    .ToList();
            var tags = db.PostTags.ToList();

            ViewData["Tags"] = new SelectList(tags, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", null, "ParentName");

			return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(BlogPostCreateCommand command)
        {
			
			//if (ModelState.IsValid)
			//{
			//	string fileExtension = Path.GetExtension(file.FileName);

			//	string name = $"blog-{Guid.NewGuid()}{fileExtension}";
			//	string physcialPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "blog", name);

			//	using (var fs = new FileStream(physcialPath, FileMode.Create, FileAccess.Write))
			//	{
			//		file.CopyTo(fs);
			//	}
			//	blogPost.ImagePath = name;

			//	db.Add(blogPost);
			//	int affected = await db.SaveChangesAsync();

			//	if (affected > 0 && tagIds != null && tagIds.Length > 0)
			//	{
			//		foreach (var item in tagIds)
			//		{
			//			db.BlogPostTags.Add(new BlogPostTag
			//			{
			//				BlogPostId = blogPost.Id,
			//				PostTagId = item,
			//			});
			//		}

			//		await db.SaveChangesAsync();
			//	}

			//	return RedirectToAction(nameof(Index));
			//}

            var response = await mediator.Send(command);
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }




			var tags = db.PostTags.ToList();

            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", command.CategoryId, "ParentName");
            ViewData["Tags"] = new SelectList(tags, "Id", "Name");

            return View(command);
        }

        // GET: Admin/BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            var categories = db.Categories
                    .Select(BindParentName)
                    .ToList();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", blogPost.CategoryId, "ParentName");

            return View(blogPost);
        }

        // POST: Admin/BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost blogPost, IFormFile file,int[] tagIds)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if(file==null && string.IsNullOrWhiteSpace(blogPost.ImagePath))
				ModelState.AddModelError("ImagePath", "No file choosen!"); 

            if (ModelState.IsValid)
            {
                try
                {
                    var currentEntity = db.BlogPosts.FirstOrDefault(bp => bp.Id == blogPost.Id);

                    if (currentEntity == null)
                        return NotFound();

                    if (file != null)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);

                        string name = $"blog-{Guid.NewGuid()}{fileExtension}";
                        string physcialPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "blog", name);

                        using (var fs = new FileStream(physcialPath, FileMode.Create, FileAccess.Write))
                        {
                            file.CopyTo(fs);
                        }
                        currentEntity.ImagePath = name;

                        string oldPhyscialPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "blog", blogPost.ImagePath);

                        if (System.IO.File.Exists(oldPhyscialPath))
                            System.IO.File.Delete(oldPhyscialPath);
                    }

                    currentEntity.Title = blogPost.Title;
                    currentEntity.CategoryId = blogPost.CategoryId;
                    currentEntity.Body = blogPost.Body;

                    if (tagIds != null && tagIds.Length > 0)
                    {
                        

                        foreach (var item in tagIds)
                        {
                            if (db.BlogPostTags.Any(bpt => bpt.PostTagId == item && bpt.BlogPostId == blogPost.Id))
                            {
                                continue;
                            }
                            db.BlogPostTags.Add(new BlogPostTag
                            {
                                BlogPostId = blogPost.Id,
                                PostTagId = item,
                            });
                        }

                        await db.SaveChangesAsync();
                    }

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
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
            var categories = db.Categories
                  .Select(BindParentName)
                  .ToList();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", blogPost.CategoryId, "ParentName");

            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Delete/5
        public IActionResult Delete([FromRoute] int id)
        {
            var entity = db.BlogPosts.FirstOrDefault(c => c.Id == id);
            if (entity == null)
            {
                return Json(new
                {
                    error = true,
                    message = "It does not exist!"
                });
            }

            entity.DeletedById = 1; //todo
            entity.DeletedDate = DateTime.UtcNow.AddHours(4);
            db.SaveChanges();

            return Json(new
            {
                error = false,
                message = "Deleted!"
            });

        }
        private bool BlogPostExists(int id)
        {
            return db.BlogPosts.Any(e => e.Id == id);
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
