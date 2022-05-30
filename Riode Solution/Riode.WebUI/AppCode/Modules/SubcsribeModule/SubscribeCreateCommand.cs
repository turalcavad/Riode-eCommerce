using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.Models.DataContexts;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Modules.SubcsribeModule
{
	public class SubscribeCreateCommand : IRequest<CommandJsonResponse>
	{
		public string Email { get; set; }
		public class SubscribeCreateCommandHandler : IRequestHandler<SubscribeCreateCommand, CommandJsonResponse>
		{
			readonly RiodeDbContext db;
			readonly IConfiguration cfg;
			readonly IActionContextAccessor ctx;
			public SubscribeCreateCommandHandler(RiodeDbContext db, IConfiguration cfg, IActionContextAccessor ctx)
			{
				this.db = db;
				this.cfg = cfg;
				this.ctx = ctx;
			}
			public async Task<CommandJsonResponse> Handle(SubscribeCreateCommand request, CancellationToken cancellationToken)
			{
				var displayName = cfg["emailAccount:displayName"];
				var smtpServer = cfg["emailAccount:smtpServer"];
				var smtpPort = Convert.ToInt32(cfg["emailAccount:smtpPort"]);
				var userName = cfg["emailAccount:userName"];
				var password = cfg["emailAccount:password"];
				var cc = cfg["emailAccount:cc"];

				var subscribe = await db.Subscribes.FirstOrDefaultAsync(s => s.Email.Equals(request.Email),cancellationToken);

				if (true)
				{
					subscribe = new Subscribe();
					subscribe.Email = request.Email;
					await db.Subscribes.AddAsync(subscribe, cancellationToken);
					await db.SaveChangesAsync(cancellationToken);
				}
				else if (subscribe.EmailSent == true)
				{
					return new CommandJsonResponse
					{
						Error = true,
						Message = "You have already subscribed with this email address!",
					};
				}
				

				try
				{
					string token = $"{subscribe.Id}-{subscribe.Email}".Encrypt("demo");
					string link = $"{ctx.GetAppLink()}/subscribe-confirm?token={token}";

					SmtpClient client = new SmtpClient(smtpServer, smtpPort);
					client.Credentials = new NetworkCredential(userName, password);
					client.EnableSsl = true;

					var from = new MailAddress(userName, displayName);
					MailMessage message = new MailMessage(from, new MailAddress(subscribe.Email));

					message.Subject = "Riode Subscription confirmation";
					message.Body = $"Please confirm your subscription with this <a href=\"{link}\">link</a>";
					message.IsBodyHtml = true;

					message.Bcc.Add(cc);
					client.SendAsync(message, cancellationToken);

					subscribe.EmailSent = true;
					await db.SaveChangesAsync(cancellationToken);

					return new CommandJsonResponse
					{
						Error = false,
						Message = "Confirmation link has been sent to your email address!"
					};
				}
				catch (Exception)
				{
					return new CommandJsonResponse
					{
						Error = true,
						Message = "Something went wrong!",
					};
				}
			}
		}
	}
}
