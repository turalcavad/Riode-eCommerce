using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Riode.Business.Modules.BrandModule;
using Riode.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly IMapper mapper;

		public BrandsController(IMediator mediator, IMapper mapper)
		{
			this.mediator = mediator;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromRoute] BrandsAllQuery query)
		{
			var response = await mediator.Send(query);

			//var dtoModel = response.Select(b=> new BrandDto
			//{
			//	Id = b.Id,
			//	Name = b.Name,
			//	AuthorId = b.CreatedById,
			//	AuthorName = b.CreatedBy?.Email
			//});

			var dtoModel = mapper.Map<List<BrandDto>>(response);

			return Ok(dtoModel);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] BrandSingleQuery query)
		{
			var response = await mediator.Send(query);

			//var dtoModel = new BrandDto();

			//dtoModel.Id = response.Id;
			//dtoModel.Name = response.Name;
			//dtoModel.AuthorId = response.CreatedById;
			//dtoModel.AuthorName = response.CreatedBy?.Email;

			var dtoModel = mapper.Map<BrandDto>(response);

			return Ok(dtoModel);
		}
		
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] BrandCreateCommand command)
		{
			var response = await mediator.Send(command);

			return CreatedAtAction(nameof(Get), routeValues: new { id = response.Id }, response);
		}
		
		[HttpPut("{Id}")]
		public async Task<IActionResult> Edit([FromBody] BrandEditCommand command)
		{
			var response = await mediator.Send(command);

			return Ok(response);
		}
		
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete([FromRoute] BrandRemoveCommand command)
		{
			var response = await mediator.Send(command);

			return Ok(response);
		}
	}
}
