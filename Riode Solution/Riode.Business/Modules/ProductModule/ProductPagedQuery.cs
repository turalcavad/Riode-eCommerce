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

namespace Riode.Business.Modules.ProductModule
{
	public class ProductPagedQuery : PaginateModel ,IRequest<PagedViewModel<Product>>
	{
		public class ProductPagedQueryHandler : IRequestHandler<ProductPagedQuery, PagedViewModel<Product>>
		{
			private readonly RiodeDbContext db;

			public ProductPagedQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<PagedViewModel<Product>> Handle(ProductPagedQuery request, CancellationToken cancellationToken)
			{

				var query = db.Products
					.Include(p => p.Brand)
					.Include(p => p.Category)
					.Include(p => p.Images.Where(i => i.IsMain == true))
					.Where(p=> p.DeletedById == null);

				var pagedData = new PagedViewModel<Product>(query, request.PageIndex, request.PageSize);

				return pagedData;
			}
		}
	}
}
