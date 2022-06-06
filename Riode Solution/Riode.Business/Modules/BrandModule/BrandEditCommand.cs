using MediatR;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.BrandModule
{
	public class BrandEditCommand : IRequest<Brand>
	{
		public string Name { get; set; }
		public int Id { get; set; }

		public class BrandEditCommandHandler : IRequestHandler<BrandEditCommand, Brand>
		{
			readonly RiodeDbContext db;
			public BrandEditCommandHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<Brand> Handle(BrandEditCommand request, CancellationToken cancellationToken)
			{
				var entity = db.Brands.FirstOrDefault(b => request.Id == b.Id && b.DeletedById == null);

				if (entity == null)
					return null;

				entity.Name = request.Name;

				await db.SaveChangesAsync(cancellationToken);
				return entity;
			}
		}
	}
}
