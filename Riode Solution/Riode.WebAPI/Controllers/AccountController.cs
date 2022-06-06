using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Riode.Business.Modules.AccountModule;
using Riode.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riode.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly IConfiguration configuration;

		public AccountController(IMediator mediator, IConfiguration configuration)
		{
			this.mediator = mediator;
			this.configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost("signin")]
		public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
		{
			var response = await mediator.Send(command);

			if (!ModelState.IsValid)
				return Ok(ModelState);

			Guid randomId = Guid.NewGuid();

			var claims = new List<Claim>
							{
								new Claim(JwtRegisteredClaimNames.Jti, randomId.ToString()),
								new Claim(JwtRegisteredClaimNames.Email, response.Email),
								new Claim(ClaimTypes.NameIdentifier, response.Id.ToString())
							};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:saltKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			//var expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["jwt:expiredMinutes"]));
			var expires = DateTime.Now.AddMonths(5);

			var tokenObject = new JwtSecurityToken(
				configuration["jwt:issuer"],
				configuration["jwt:audience"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return Ok(new { 
				token = new JwtSecurityTokenHandler().WriteToken(tokenObject),
				sessionId = randomId,
				FullName = $"{response.Name} {response.Surname}" 

			});
		}

	}
}
