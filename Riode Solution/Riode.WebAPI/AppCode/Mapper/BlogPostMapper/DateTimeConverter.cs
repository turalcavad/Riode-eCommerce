using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebAPI.AppCode.Mapper.BlogPostMapper
{
	public class DateTimeConverter : IValueConverter<DateTime, string>
	{
		public string Convert(DateTime sourceMember, ResolutionContext context)
		{
			var dateFormat = "dd.MM.yyyy HH:mm";

			try
			{
				if (context.Items.ContainsKey("dateFormat"))
				{
					dateFormat = context.Items["dateFormat"] as string;
				}
			}
			catch { }

			return sourceMember.ToString(dateFormat);
		}
	}
}
