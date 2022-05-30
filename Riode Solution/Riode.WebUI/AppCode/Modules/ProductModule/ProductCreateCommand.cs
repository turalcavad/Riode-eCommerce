using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using me = Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Modules.ProductModule
{
	public class ProductCreateCommand : IRequest<ProductCreateCommandResponse>
	{
		public string Name { get; set; }
		public string StockKeepingUnit { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public SpecificationKeyValue[] Specification { get; set; }
		public ProductPricing[] Pricing { get; set; }
		public ImageItem[] Images { get; set; }
		public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductCreateCommandResponse>
		{

			readonly RiodeDbContext db;
			readonly IActionContextAccessor ctx;
			readonly IWebHostEnvironment env;
			readonly IValidator<ProductCreateCommand> validator;

			public ProductCreateCommandHandler(
				RiodeDbContext db, 
				IActionContextAccessor ctx,
				IWebHostEnvironment env,
				IValidator<ProductCreateCommand> validator)
			{
				this.db = db;
				this.ctx = ctx;
				this.env = env;
				this.validator = validator;
			}
			public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
			{
				var result = validator.Validate(request);

				if (!result.IsValid)
				{
					var response = new ProductCreateCommandResponse
					{
						Product = null,
						ValidationResult = result
					};

					return response;
				}

			/*	if (!ctx.ModelIsValid())
					return null;*/

				var product = new Product
				{
					Name = request.Name,
					BrandId = request.BrandId,
					CategoryId = request.CategoryId,
					Description = request.Description,
					ShortDescription = request.ShortDescription,
					StockKeepingUnit = request.StockKeepingUnit
				};


				if (request.Specification != null && request.Specification.Length > 0)
				{
					product.Specifications = new List<ProductSpecification>();

					foreach (var spec in request.Specification)
					{
						product.Specifications.Add(new ProductSpecification
						{
							SpecificationId = spec.Id,
							Value = spec.Value
						});
					}
				}

				if (request.Images != null && request.Images.Any(i => i.File != null))
				{
					product.Images = new List<ProductImage>();
					foreach (var image in request.Images.Where(i => i.File != null))
					{

						string name = await env.SaveFile(image.File, cancellationToken, "product");
						product.Images.Add(new ProductImage
						{
							ImagePath = name,
							IsMain = image.IsMain
						});

					}
				}
				else {
					ctx.AddModelError("Images", "No file choosen!");
					goto l1;
				}

				if (request.Pricing != null && request.Pricing.Length > 0)
				{
					product.Pricing = new List<me.ProductPricing>();

					foreach (var pricing in request.Pricing)
					{
						product.Pricing.Add(new me.ProductPricing
						{
							ColorId = pricing.ColorId,
							SizeId = pricing.SizeId,
							Price = pricing.Price

						});
					}
				}

				await db.Products.AddAsync(product,cancellationToken);

				try
				{
					await db.SaveChangesAsync(cancellationToken);

					var response = new ProductCreateCommandResponse
					{
						Product = product
					};
					return response;
				}
				catch (Exception)
				{
					var response = new ProductCreateCommandResponse
					{
						Product = product,
						ValidationResult = result
					};

					response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Something went wrong, please try again later!"));
					return response;
				}

				l1:
				return null;
			}
		}

		
	}

	public class ProductCreateCommandResponse 
	{
		public Product Product { get; set; }
		public ValidationResult ValidationResult { get; set; }
	}


}
