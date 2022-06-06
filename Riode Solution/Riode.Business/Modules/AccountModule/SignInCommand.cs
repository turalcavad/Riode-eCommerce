using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Core.Extensions;
using Riode.Data.DataContexts;
using Riode.Data.Entities.Membership;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.AccountModule
{
	public class SignInCommand : IRequest<RiodeUser>
	{
		public string UserName { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public class SignInCommandHandler : IRequestHandler<SignInCommand, RiodeUser>
		{
			private readonly UserManager<RiodeUser> userManager;
			private readonly SignInManager<RiodeUser> signInManager;
			private readonly IActionContextAccessor ctx;
			private readonly RiodeDbContext db;

			public SignInCommandHandler(UserManager<RiodeUser> userManager,
					SignInManager<RiodeUser> signInManager,
					IActionContextAccessor ctx,
					RiodeDbContext db)
			{
				this.userManager = userManager;
				this.signInManager = signInManager;
				this.ctx = ctx;
				this.db = db;
			}
			public async Task<RiodeUser> Handle(SignInCommand request, CancellationToken cancellationToken)
			{
				if (!ctx.ModelIsValid())
					return null;


				RiodeUser currentUser = null;

				if (request.UserName.IsEmail())
				{
					currentUser = await userManager.FindByEmailAsync(request.UserName);
				}
				else
				{
					currentUser = await userManager.FindByNameAsync(request.UserName);
				}

				if (currentUser == null)
				{
					ctx.AddModelError("UserName", "Username or password is incorrect!");
					return null;
				}

				var signInResult = await signInManager.PasswordSignInAsync(currentUser, request.Password, true, true);

				if (!signInResult.Succeeded)
				{
					ctx.AddModelError("UserName", "Username or password is incorrect!");
					return null;

				}

				return currentUser;
			}
		}
	}
}
