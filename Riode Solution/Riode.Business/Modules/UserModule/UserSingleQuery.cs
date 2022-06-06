using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Data.DataContexts;
using Riode.Data.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.UserModule
{
	public class UserSingleQuery : IRequest<RiodeUser>
	{
		public int Id { get; set; }
		public class UserSingleQueryHandler : IRequestHandler<UserSingleQuery, RiodeUser>
		{
			private readonly RiodeDbContext db;

			public UserSingleQueryHandler(RiodeDbContext db)
			{
				this.db = db;
			}
			public async Task<RiodeUser> Handle(UserSingleQuery request, CancellationToken cancellationToken)
			{
				var query = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

				return query;
			}
		}
	}
}
