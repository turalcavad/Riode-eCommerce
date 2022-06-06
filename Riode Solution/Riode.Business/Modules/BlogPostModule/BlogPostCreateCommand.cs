using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Core.Extensions;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.BlogPostModule
{
	public class BlogPostCreateCommand : IRequest<BlogPost>
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public string ImagePath { get; set; }
		public int CategoryId { get; set; }
		public IFormFile File { get; set; }
		public int[] TagIds { get; set; }

		public class BlogPostCreateCommandHandler : IRequestHandler<BlogPostCreateCommand, BlogPost>
		{
			readonly RiodeDbContext db;
			readonly IHostingEnvironment env;
			readonly IActionContextAccessor ctx;
			public BlogPostCreateCommandHandler(RiodeDbContext db, IHostingEnvironment env, IActionContextAccessor ctx)
			{
				this.db = db;
				this.env = env;
				this.ctx = ctx;
			}
			public async Task<BlogPost> Handle(BlogPostCreateCommand request, CancellationToken cancellationToken)
			{

				if (request?.File == null)
				{
					ctx.AddModelError("ImagePath", "No file choosen!");
					//ModelState.AddModelError("ImagePath", "No file choosen!");
				}
				if (ctx.ModelIsValid())
				{
					string fileExtension = Path.GetExtension(request.File.FileName);

					string name = $"blog-{Guid.NewGuid()}{fileExtension}";
					string physcialPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", "blog", name);

					using (var fs = new FileStream(physcialPath, FileMode.Create, FileAccess.Write))
					{
						request.File.CopyTo(fs);
					}
					var blog = new BlogPost
					{
						Title = request.Title,
						Body = request.Body,
						CategoryId = request.CategoryId,
						ImagePath = name
					};

					db.BlogPosts.Add(blog);
					int affected = await db.SaveChangesAsync(cancellationToken);
					
					if (affected > 0 && request.TagIds != null && request.TagIds.Length > 0)
					{
						foreach (var item in request.TagIds)
						{
							db.BlogPostTags.Add(new BlogPostTag
							{
								BlogPostId = blog.Id,
								PostTagId = item,
							});
						}

						await db.SaveChangesAsync(cancellationToken);
					}

					return blog;

					
				}

				return null;
				

			}


			


		}
	}
}
