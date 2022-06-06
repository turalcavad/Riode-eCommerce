using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.BrandModule
{
	public class BrandSingleQuery : IRequest<Brand>
	{
		public int Id { get; set; }


		public class BrandSingleQueryHandler : IRequestHandler<BrandSingleQuery, Brand>
		{
			readonly RiodeDbContext db;
			public BrandSingleQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<Brand> Handle(BrandSingleQuery request, CancellationToken cancellationToken)
			{
				var model = await db.Brands
					.Include(b=> b.CreatedBy)
					.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

				return model;
			}
		}
	}
}
