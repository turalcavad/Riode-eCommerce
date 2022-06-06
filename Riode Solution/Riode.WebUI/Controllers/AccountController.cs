using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Riode.Business.Modules.AccountModule;
using Riode.Core.Extensions;
using Riode.Data.Entities.Membership;
using Riode.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<RiodeUser> userManager;
		private readonly SignInManager<RiodeUser> signInManager;
		private readonly IConfiguration configuration;
		private readonly IActionContextAccessor ctx;
		private readonly IMediator mediator;

		public AccountController(UserManager<RiodeUser> userManager,
			SignInManager<RiodeUser> signInManager,
			IConfiguration configuration,
			IActionContextAccessor ctx,
			IMediator mediator)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.configuration = configuration;
			this.ctx = ctx;
			this.mediator = mediator;
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("/signin.html")]
		public IActionResult SignIn()
		{
			return View();
		}
		
		[HttpPost]
		[AllowAnonymous]
		[Route("/signin.html")]
		public async Task<IActionResult> SignIn(SignInCommand command)
		{
			//	#region Controller base

			//	if (ModelState.IsValid)
			//	{
			//		RiodeUser currentUser = null;

			//		if (user.UserName.IsEmail())
			//		{
			//			currentUser = await userManager.FindByEmailAsync(user.UserName);
			//		}
			//		else
			//		{
			//			currentUser = await userManager.FindByNameAsync(user.UserName);
			//		}

			//		if (currentUser == null)
			//		{
			//			ViewBag.Message = "Username or password is incorrect!";
			//			goto end;
			//		}

			//		var signInResult = await signInManager.PasswordSignInAsync(currentUser, user.Password, true, true);

			//		if (!signInResult.Succeeded)
			//		{
			//			ViewBag.Message = "Username or password is incorrect!";
			//			goto end;
			//		}

			//		var callbackUrl = Request.Query["ReturnUrl"];

			//		if (!string.IsNullOrWhiteSpace(callbackUrl))
			//			return Redirect(callbackUrl);

			//		return RedirectToAction("Index", "Shop");
			//	}
			//#endregion
			var response = await mediator.Send(command);

			var callbackUrl = Request.Query["ReturnUrl"];
			//var error = ModelState.GetError();
			//ViewBag.Message = error;

			if (ModelState.IsValid)
			{
				if (!string.IsNullOrWhiteSpace(callbackUrl))
					return Redirect(callbackUrl);

				return RedirectToAction("Index", "Shop");
			}


			return View();

		}

		[AllowAnonymous]
		[HttpGet]
		[Route("/register.html")]
		public IActionResult Register()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		[Route("/register.html")]
		public async Task<IActionResult> Register(RegisterFormModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new RiodeUser();

				user.Email = model.Email;
				user.UserName = model.Email;
				user.Surname = model.Surname;
				user.Name = model.Name;
			
				//user.EmailConfirmed = true;
				

				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{

					var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
					string link = $"{ctx.GetAppLink()}/registration-confirm.html?email={user.Email}&token={token}";

					var emailResponse = configuration.SendEmail(user.Email, "Riode User Registration", $"Please confirm your subscription with this <a href=\"{link}\">link</a>");

					if (emailResponse)
					{
						ViewBag.Message = "Your account has been created!";
					}
					else 
					{ 
						ViewBag.Message = "Something went wrong, please try again later!";
					}

					return RedirectToAction(nameof(SignIn));
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
			}

			return View(model);
		}

		[AllowAnonymous]
		[Route("/registration-confirm.html")]
		public async Task<IActionResult> RegisterConfirm(string email, string token)
		{
			var currentUser = await userManager.FindByEmailAsync(email);

			if (currentUser == null)
			{
				ViewBag.Message = "Damaged Token!";
				goto end;
			}

			token = token.Replace(" ", "+");

			var result = await userManager.ConfirmEmailAsync(currentUser, token);

			if (!result.Succeeded)
			{
				ViewBag.Message = "Damaged Token!";
				goto end;
			}
		end:
			return RedirectToAction(nameof(SignIn));
		}


		public IActionResult Profile(string email, string token)
		{
			return View();
		}

		[Route("/logout.html")]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}

		[Route("/accessdenied.html")]
		[AllowAnonymous]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
