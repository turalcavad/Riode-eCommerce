using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Business.Modules.ProductModule;
using Riode.Core.Infrastructure;
using Riode.Data.Entities;
using Riode.WebAPI.AppCode.Mapper.ProductMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly IMapper mapper;
		private readonly IActionContextAccessor ctx;

		public ProductsController(IMediator mediator, IMapper mapper, IActionContextAccessor ctx)
		{
			this.mediator = mediator;
			this.mapper = mapper;
			this.ctx = ctx;
		}

		[HttpGet("{PageIndex}/{PageSize}")]
		public async Task<IActionResult> Get([FromRoute] ProductPagedQuery query)
		{
			var response = await mediator.Send(query);

			var dtoModel = mapper.Map<PagedViewModel<ProductDto>>(response, cfg=> 
			{
				cfg.Items.Add("ctx", ctx);			
			});

			return Ok(dtoModel);
		}


		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] ProductSingleQuery query)
		{
			var response = await mediator.Send(query);
			
			var dtoModel = mapper.Map<ProductDto>(response,cfg=>{
				cfg.Items.Add("ctx", ctx);
			});

			return Ok(dtoModel);
		}
		
		[HttpPost]
		public async Task<IActionResult> Add([FromForm] ProductCreateCommand command)
		{
			var response = await mediator.Send(command);

			if (!response.ValidationResult.IsValid)
			{
				return BadRequest(response.ValidationResult);
			}
			return CreatedAtAction("Get", routeValues:new {Id = response.Product.Id }, response.Product);
		}
	}
}
