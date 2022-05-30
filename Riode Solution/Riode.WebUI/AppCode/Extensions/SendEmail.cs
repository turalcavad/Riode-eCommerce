using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Configuration;
using Riode.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{
		public static bool SendEmail(this IConfiguration cfg,
			string to,
			string subject,
			string body,
			bool appendCC = false
			)
		{
			try
			{
				var displayName = cfg["emailAccount:displayName"];
				var smtpServer = cfg["emailAccount:smtpServer"];
				var smtpPort = Convert.ToInt32(cfg["emailAccount:smtpPort"]);
				var fromMail = cfg["emailAccount:userName"];
				var password = cfg["emailAccount:password"];
				var cc = cfg["emailAccount:cc"];


				using (MailMessage message = new MailMessage(new MailAddress(fromMail, displayName), new MailAddress(to))
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				})
				{
					if (string.IsNullOrWhiteSpace(cc) && appendCC)
						message.CC.Add(cc);

					SmtpClient smptpClient = new SmtpClient(smtpServer, smtpPort);
					smptpClient.Credentials = new NetworkCredential(fromMail, password);
					smptpClient.EnableSsl = true;
					smptpClient.Send(message);
				}
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}
			
	}
}
