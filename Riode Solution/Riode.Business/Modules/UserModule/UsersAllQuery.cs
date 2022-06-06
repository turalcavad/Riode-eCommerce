using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Core.Infrastructure;
using Riode.Data.DataContexts;
using Riode.Data.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.UserModule
{
	public class UsersAllQuery : IRequest<PagedViewModel<RiodeUser>>
	{
		public class UsersAllQueryHandler : IRequestHandler<UsersAllQuery, PagedViewModel<RiodeUser>>
		{
			private readonly RiodeDbContext db;
			

			public UsersAllQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<PagedViewModel<RiodeUser>> Handle(UsersAllQuery request, CancellationToken cancellationToken)
			{
				var query = db.Users;

				var pagedViewModel = new PagedViewModel<RiodeUser>(query,1,10);
				return pagedViewModel;
			}
		}
	}
}
