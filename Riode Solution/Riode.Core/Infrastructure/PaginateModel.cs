using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riode.Core.Infrastructure
{
	public abstract class PaginateModel
	{
		int pageIndex;
		int pageSize;

		public int PageIndex
		{
			get 
			{
				return pageIndex <= 0 ? 1 : pageIndex;
			}

			set 
			{
				if (value > 0)
				{
					pageIndex = value;
				}
			}
		}
		public int PageSize
		{
			get 
			{
				return pageSize <= 0 ? 15 : pageSize;
			}

			set 
			{
				if (value > 15)
				{
					pageSize = value;
				}
			}
		}
	}
}
