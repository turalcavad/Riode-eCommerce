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
	public class ColorSingleQuery : IRequest<ProductColor>
	{
		public int Id { get; set; }

		public class ColorSingleQueryHandler : IRequestHandler<ColorSingleQuery, ProductColor>
		{
			readonly RiodeDbContext db;
			public ColorSingleQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<ProductColor> Handle(ColorSingleQuery request, CancellationToken cancellationToken)
			{
				var entity = await db.Colors.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedById == null, cancellationToken);
				return entity;
			}
		}
	}
}
