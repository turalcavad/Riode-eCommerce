using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Core.Infrastructure;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.BlogPostModule
{
	public class BlogSingleQuery :  IRequest<BlogPost>
	{
		public int Id { get; set; }
		public class BlogSingleQueryHandler : IRequestHandler<BlogSingleQuery, BlogPost>
		{
			private readonly RiodeDbContext db;
			

			public BlogSingleQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<BlogPost> Handle(BlogSingleQuery request, CancellationToken cancellationToken)
			{
				var post = await db.BlogPosts
					.Include(b => b.Category)
					//.Include(b=> b.CreatedBy) ???
					.FirstOrDefaultAsync(b => b.DeletedById == null && b.Id == request.Id, cancellationToken);

				return post;
			}
		}
	}
}
