using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
	public class ProductEditCommand : IRequest<ProductEditCommandResponse>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string StockKeepingUnit { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public SpecificationKeyValue[] Specification { get; set; }
		public ProductPricing[] Pricing { get; set; }
		public ImageItem[] Images { get; set; }
		public class ProductEditCommandHandler : IRequestHandler<ProductEditCommand, ProductEditCommandResponse>
		{

			readonly RiodeDbContext db;
			readonly IActionContextAccessor ctx;
			readonly IWebHostEnvironment env;
			readonly IValidator<ProductEditCommand> validator;

			public ProductEditCommandHandler(
				RiodeDbContext db, 
				IActionContextAccessor ctx,
				IWebHostEnvironment env,
				IValidator<ProductEditCommand> validator)
			{
				this.db = db;
				this.ctx = ctx;
				this.env = env;
				this.validator = validator;
			}
			public async Task<ProductEditCommandResponse> Handle(ProductEditCommand request, CancellationToken cancellationToken)
			{
				var result = validator.Validate(request);

				var response = new ProductEditCommandResponse
				{
					Product = null,
					ValidationResult = null
				};

				if (!result.IsValid)
				{
					response.ValidationResult = result;

					return response;
				}

				/*	if (!ctx.ModelIsValid())
						return null;*/

				var product = await db.Products
					.Include(p=> p.Images)
					.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

				if (product == null)
				{
					response.ValidationResult = new ValidationResult();
					response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Product does not exist!"));
				}

				product.Name = request.Name;
				product.BrandId = request.BrandId;
				product.CategoryId = request.CategoryId;
				product.Description = request.Description;
				product.ShortDescription = request.ShortDescription;
				product.StockKeepingUnit = request.StockKeepingUnit;
				



				if (request.Images != null)
				{
					if (product.Images == null)
					{ 
						product.Images = new List<ProductImage>();
					}
					
					foreach (var image in request.Images.Where(i => i.File != null))
					{
						if (image.File == null && string.IsNullOrWhiteSpace(image.TempPath)) //to delete image
						{
							var dbProductImage = product.Images.FirstOrDefault(p => p.Id == image.Id);

							if (dbProductImage != null)
							{
								dbProductImage.DeletedDate = DateTime.UtcNow.AddHours(4);
								dbProductImage.DeletedById = 1;
								dbProductImage.IsMain = false;
							}
						}
						else if (image.File != null) //to add new image
						{
							string name = await env.SaveFile(image.File, cancellationToken, "product");
							product.Images.Add(new ProductImage
							{
								ImagePath = name,
								IsMain = image.IsMain
							});
						}
						else if (!string.IsNullOrWhiteSpace(image.TempPath)) // to change "isMain" only
						{
							var dbProductImage = product.Images.FirstOrDefault(p => p.Id == image.Id);

							if (dbProductImage != null)
							{
								dbProductImage.IsMain = image.IsMain;
							}
						}
						

					}
				}
				else {
					ctx.AddModelError("Images", "No file choosen!");
					goto l1;
				}

				if (request.Specification != null && request.Specification.Length > 0)
				{

					if (product.Specifications == null)
					{ 
						product.Specifications = new List<ProductSpecification>();
					}


					foreach (var spec in product.Specifications)
					{
						var specFromForm = request.Specification.FirstOrDefault(s => s.Id == spec.SpecificationId);

						if (specFromForm == null || string.IsNullOrWhiteSpace(specFromForm.Value))
						{
							db.ProductSpecifications.Remove(spec);
						}
						else 
						{
							spec.Value = specFromForm.Value;
						}

						product.Specifications.Add(new ProductSpecification
						{
							SpecificationId = spec.SpecificationId,
							Value = spec.Value
						});
					}

					var ids = request.Specification.Select(s => s.Id)
						.Except(product.Specifications.Select(s => s.SpecificationId));

					foreach (var id in ids)
					{
						var spec = request.Specification.FirstOrDefault(s => s.Id == id);

						product.Specifications.Add(new ProductSpecification
						{
							SpecificationId = spec.Id,
							Value = spec.Value
						});
					}
				}

				//if (request.Pricing != null && request.Pricing.Length > 0)
				//{
				//	product.Pricing = new List<me.ProductPricing>();

				//	foreach (var pricing in request.Pricing)
				//	{
				//		product.Pricing.Add(new me.ProductPricing
				//		{
				//			ColorId = pricing.ColorId,
				//			SizeId = pricing.SizeId,
				//			Price = pricing.Price

				//		});
				//	}
				//}



				try
				{
					await db.SaveChangesAsync(cancellationToken);

					response.Product = product;
					response.ValidationResult = result;
					return response;
				}
				catch (Exception)
				{
					response.Product = product;
					response.ValidationResult = result;

					response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Something went wrong, please try again later!"));
					return response;
				}

				l1:
				return null;
			}
		}

		
	}

	
	public class ProductEditCommandResponse 
	{
		public Product Product { get; set; }
		public ValidationResult ValidationResult { get; set; }
	}


}
