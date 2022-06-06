using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using Riode.Business.Modules.BlogPostModule;
using Riode.Core.Extensions;
using Riode.Core.Infrastructure;
using Riode.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly IMapper mapper;
		private readonly IActionContextAccessor ctx;

		public BlogPostsController(IMediator mediator, IMapper mapper, IActionContextAccessor ctx)
		{
			this.mediator = mediator;
			this.mapper = mapper;
			this.ctx = ctx;
		}

		[HttpGet("{PageIndex}/{PageSize}")]
		public async Task<IActionResult> Get([FromRoute] BlogPostPagedQuery query)
		{
			var response = await mediator.Send(query);

			var dtoModel = mapper.Map<PagedViewModel<BlogPostDto>>(response, cfg=>
			{
				cfg.Items.Add("ctx", ctx);
			});

			return Ok(dtoModel);
		}
		
		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] BlogSingleQuery query)
		{
			var response = await mediator.Send(query);

			var dtoModel = mapper.Map<BlogPostSingleDto>(response, cfg =>
			{
				cfg.Items.Add("ctx", ctx);

				Request.CopyTo("dateFormat", cfg.Items);
			});

			return Ok(dtoModel);
		}
	}
}
