﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{
		static public string HtmlToPlainText(this string html)
		{
			if (string.IsNullOrWhiteSpace(html))
				return html;

			html = Regex.Replace(html, "<(.|\n)*?>", "");
			html = Regex.Replace(html, @"\s+", " ");

			return html;

		}

		static public string ToEllipse(this string text, int length = 50)
		{
			if (string.IsNullOrWhiteSpace(text) || length >= text.Length)
				return text;

			return $"{text.Substring(0, length)}...";
		}	
	}
}
