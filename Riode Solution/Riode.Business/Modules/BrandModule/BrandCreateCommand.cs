using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.BrandModule
{
	public class BrandCreateCommand : IRequest<Brand>
	{
		public string Name { get; set; }

		public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, Brand>
		{
			readonly RiodeDbContext db;
			private readonly IActionContextAccessor ctx;

			public BrandCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
			{
				this.db = db;
				this.ctx = ctx;
			}
			public async Task<Brand> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
			{
				var brand = new Brand();
				brand.Name = request.Name;
				//brand.CreatedById = ctx.GetUserId()

				await db.Brands.AddAsync(brand);

				await db.SaveChangesAsync(cancellationToken);
				return brand;
			}
		}
	}
}
