using Microsoft.AspNetCore.Html;
using Riode.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riode.Core.Extensions
{
	public static partial class Extension
	{
		public static HtmlString GetCategoriesRaw(this List<Category> categories)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<ul class=\"widget-body filter-items search-ul\">");


			foreach (var category in categories.Where(c=>c.ParentId==null))
			{
				AppendCategory(category, sb);
			}

			sb.Append("</ul>");

			return new HtmlString(sb.ToString());

		}

		static void AppendCategory(Category category, StringBuilder sb)
		{
			bool hasChild = category.Children.Any();
			sb.Append($"<li{(hasChild ? " class=\"with-ul\"" : "")}><a href=\"#\">{category.Name}");

			if (hasChild)
			{
				sb.Append("<i class=\"fas fa-chevron-down\"></i>");
			}
			sb.Append("</a>");

			if (hasChild)
			{ 
				sb.Append("<ul>");

				foreach (var child in category.Children)
				{
					AppendCategory(child, sb);
				}
				sb.Append("</ul>");

			}


			sb.Append("</li>");

		}

		static public IEnumerable<Category> GetChildren(this Category category) 
		{
			if(category.ParentId!= null)
				yield return category;

			if (category.Children != null)
			{ 
				foreach (var item in category.Children.SelectMany(c => c.GetChildren()))
				{
					yield return item;
				}
			
			}
		}
	}
}
