using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Data.DataContexts;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.ColorModule
{
	public class ColorAllQuery : IRequest<IEnumerable<ProductColor>>
	{
		public class ColorAllQueryHandler : IRequestHandler<ColorAllQuery, IEnumerable<ProductColor>>
		{
			readonly RiodeDbContext db;
			public ColorAllQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<IEnumerable<ProductColor>> Handle(ColorAllQuery request, CancellationToken cancellationToken)
			{
				var model = await db.Colors.Where(c=> c.DeletedById == null).ToListAsync(cancellationToken);

				return model;
			}
		}
	}
}

