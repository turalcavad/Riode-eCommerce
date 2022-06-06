using MediatR;
using Microsoft.EntityFrameworkCore;
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
	public class ProductSingleQuery : IRequest<Product>
	{
		public int Id { get; set; }
		public class ProductSingleQueryHandler : IRequestHandler<ProductSingleQuery, Product>
		{
			private readonly RiodeDbContext db;

			public ProductSingleQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<Product> Handle(ProductSingleQuery request, CancellationToken cancellationToken)
			{
				var data = await db.Products
					.Include(p=> p.Brand)
					.Include(p=> p.Category)
					.Include(p=> p.Images.Where(i=> i.DeletedById == null))
					.FirstOrDefaultAsync(p => p.DeletedById == null && p.Id == request.Id, cancellationToken);

				return data;
			}
		}
	}
}
