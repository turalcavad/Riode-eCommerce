using MediatR;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.ColorModule
{
	public class ColorCreateCommand : IRequest<ProductColor>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string HexCode { get; set; }
	/*	public class ColorCreateCommandHandler : IRequestHandler<ColorCreateCommand, ProductColor>
		{
			public async Task<ProductColor> Handle(ColorCreateCommand request, CancellationToken cancellationToken)
			{
				var color = new ProductColor();

				color.Name = request.Name;
				color.HexCode = request.HexCode;
				color.Id = request.Id;
				color.CreatedDate = DateTime.UtcNow.AddHours(4);
				color.CreatedById = 1;
			}
		}*/
	}
}
