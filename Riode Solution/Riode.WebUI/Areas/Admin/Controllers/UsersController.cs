using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.AppCode.Modules.UserModule;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UsersController : Controller
	{
		private readonly RiodeDbContext db;
		private readonly IMediator mediator;

		public UsersController(RiodeDbContext db, IMediator mediator)
		{
			this.db = db;
			this.mediator = mediator;
		}
		
		public async Task<IActionResult> Index(UsersAllQuery command, int pageIndex = 1, int pageSize = 10)
		{
			var response = await mediator.Send(command);

			return View(response);
		}
		public async Task<IActionResult> Edit(UserSingleQuery query)
		{
			var user = await mediator.Send(query);

			if (user == null)
				return NotFound();

			ViewBag.Roles = await(from r in db.Roles
								  join ur in db.UserRoles on new { RoleId = r.Id, UserId = user.Id } equals new { ur.RoleId, ur.UserId } into lJoin
								  from lj in lJoin.DefaultIfEmpty()
								  select Tuple.Create(r.Id, r.Name, lj != null)).ToListAsync();


			ViewBag.Claims = (from p in Program.principals
							  join uc in db.UserClaims on new { ClaimValue = "1", ClaimType = p, UserId = user.Id } equals new { uc.ClaimValue, uc.ClaimType, uc.UserId } into lJoin
							  from lj in lJoin.DefaultIfEmpty()
							  select Tuple.Create(p, lj != null)).ToList();

			return View(user);
		}

		public async Task<IActionResult> Details(UserSingleQuery query)
		{
			var user = await mediator.Send(query);

			if (user == null)
				return NotFound();

			ViewBag.Roles = await (from r in db.Roles
								   join ur in db.UserRoles on new { RoleId = r.Id, UserId = user.Id } equals new { ur.RoleId, ur.UserId } into lJoin
								   from lj in lJoin.DefaultIfEmpty()
								   select Tuple.Create(r.Id, r.Name, lj != null)).ToListAsync();


			ViewBag.Claims = (from p in Program.principals
							  join uc in db.UserClaims on new { ClaimValue = "1", ClaimType = p, UserId = user.Id } equals new { uc.ClaimValue, uc.ClaimType, uc.UserId } into lJoin
							  from lj in lJoin.DefaultIfEmpty()
							  select Tuple.Create(p, lj != null)).ToList();
			 

			return View(user);
		}

		[HttpPost]
		//[Route("/user-set-role")]
		[Authorize(Policy = "admin.users.setrole")]
		public async Task<IActionResult> SetRole(int userId, int roleId, bool selected)
		{
			#region Check user and role
			var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
			var role = await db.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

			if (user == null)
			{
				return Json(new
				{
					error = true,
					message = "Error!"
				});
			}
			if (role == null)
			{
				return Json(new
				{
					error = true,
					message = "Error!"
				});
			}
			#endregion

			if (selected)
			{
				if (await db.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId))
				{
					return Json(new
					{
						error = true,
						message = $"{user.Name} {user.Surname} is already in role named {role.Name}"
					});
				}

				else
				{
					await db.UserRoles.AddAsync(new RiodeUserRole
					{
						UserId = user.Id,
						RoleId = role.Id
					});

					await db.SaveChangesAsync();

					return Json(new
					{
						error = false,
						message = $"User {user.Name} {user.Surname} is now in role named {role.Name}"
					});
				}
			}
			else
			{
				var userRole = await db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

				if (userRole == null)
				{
					return Json(new
					{
						error = true,
						message = $"{user.Name} {user.Surname} is not in role named {role.Name}"
					});
				}
				else
				{

					db.UserRoles.Remove(userRole);
					await db.SaveChangesAsync();

					return Json(new
					{
						error = false,
						message = $"User {user.Name} {user.Surname} is removed from role named {role.Name}"
					});
				}
			}
		}


		[HttpPost]
		//[Route("/user-set-role")]
		[Authorize(Policy = "admin.users.setclaim")]
		public async Task<IActionResult> SetClaim(int userId, string claimName, bool selected)
		{

			#region Check user and claims

			var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
			var hasClaim = Program.principals.Contains(claimName);

			int currentUserId = User.GetUserId();

			if (userId == User.GetUserId())
			{

				return Json(new
				{
					error = true,
					message = "User cannot authorize themself!"
				});
			}
				

			if (user == null)
			{
				return Json(new
				{
					error = true,
					message = "Error!"
				});
			}
			if (!hasClaim)
			{
				return Json(new
				{
					error = true,
					message = "Error!"
				});
			}
			#endregion

			if (selected)
			{
				if (await db.UserClaims.AnyAsync(uc => uc.UserId == userId && uc.ClaimType == claimName && uc.ClaimValue == "1"))
				{
					return Json(new
					{
						error = true,
						message = $"{user.Name} {user.Surname} is already in claim named {claimName}"
					});
				}
				else
				{
					await db.UserClaims.AddAsync(new RiodeUserClaim
					{
						ClaimType = claimName,
						ClaimValue = "1",
						UserId = userId
					});

					await db.SaveChangesAsync();


					return Json(new
					{
						error = false,
						message = $"User {user.Name} {user.Surname} is now in claim named {claimName}"
					});
				}
			}
			else {
				var userClaim = await db.UserClaims.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ClaimType == claimName && uc.ClaimValue == "1");

				if (userClaim == null)
				{
					return Json(new
					{
						error = true,
						message = $"{user.Name} {user.Surname} does not have claim named {claimName}"
					});
				}
				else {
					db.UserClaims.Remove(userClaim);
					await db.SaveChangesAsync();

					return Json(new
					{
						error = false,
						message = $"User {user.Name} {user.Surname} is removed from role named {claimName}"
					});
				}
			}
		}
	}
}
