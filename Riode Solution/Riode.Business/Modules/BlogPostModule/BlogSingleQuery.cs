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
	public class BlogPostPagedQuery : PaginateModel , IRequest<PagedViewModel<BlogPost>>
	{
		public class BlogPostPagedQueryHandler : IRequestHandler<BlogPostPagedQuery, PagedViewModel<BlogPost>>
		{
			private readonly RiodeDbContext db;

			public BlogPostPagedQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<PagedViewModel<BlogPost>> Handle(BlogPostPagedQuery request, CancellationToken cancellationToken)
			{
				var query = db.BlogPosts
					.Include(b => b.Category)
					//.Include(b=> b.CreatedBy) ???
					.Where(b => b.DeletedById == null);

				var pagedData = new PagedViewModel<BlogPost>(query, request.PageIndex, request.PageSize);

				return pagedData;
			}
		}
	}
}
