using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Data.DataContexts;
using Riode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class BlogsController : Controller
	{
		readonly RiodeDbContext db;
		public BlogsController(RiodeDbContext db)
		{
			this.db = db;
		}
		public IActionResult Index()
		{
			var model = db.BlogPosts
				.Where(b=> b.DeletedById == null)
				.ToList();
			return View(model);
		}

		public IActionResult Details(int id)
		{
			var viewModel = new SinglePostViewModel();


			var post = db.BlogPosts
				.Include(b=> b.Tags)
				.ThenInclude(t=> t.PostTag)
				.FirstOrDefault(b => b.Id == id && b.DeletedById == null);
			if (post == null)
				return NotFound();

			viewModel.Post = post;

			var tagIds = post.Tags.Select(t => t.PostTagId);

			//method base

			var relatedPosts = db.BlogPosts
								.Include(bp => bp.Tags)
								.Where(bp => bp.Id != id && bp.DeletedById == null && bp.Tags.Any(t => tagIds.Any(tags => tags == t.PostTagId)))
								.OrderByDescending(bp=> bp.Id)
								.Take(15)
								.ToList();

			//query base
			/*var relatedPosts = (from bp in db.BlogPosts
								join bpt in db.BlogPostTags on bp.Id equals bpt.BlogPostId
								join _id in tagIds on bpt.PostTagId equals _id
								where bp.Id != id && bp.DeletedById == null && tagIds.Any(t => t == bpt.PostTagId)
								select bp)
							   .Distinct()
							   .ToList();*/

			viewModel.RelatedPosts = relatedPosts;


			return View(viewModel);
		}

	}
}
