using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Modules.ProductModule.Validators
{
	public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
	{
		public ProductCreateValidator()
		{
			RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Cannot be empty!");
			RuleFor(p => p.ShortDescription).NotEmpty().WithMessage("Cannot be empty!");
			RuleFor(p => p.StockKeepingUnit).NotEmpty().WithMessage("Cannot be empty!");

			RuleFor(p => p.BrandId).GreaterThan(0).WithMessage("Must be greater than 0");
			RuleFor(p => p.CategoryId).GreaterThan(0).WithMessage("Must be greater than 0");

			//RuleForEach(p => p.Specification)
			//	.ChildRules(cp => 
			//	{
			//		cp.RuleFor(cpi=>cpi.Value).NotEmpty().NotNull().WithMessage("Cannot be empty!");
			//	});




		}
	}
}
