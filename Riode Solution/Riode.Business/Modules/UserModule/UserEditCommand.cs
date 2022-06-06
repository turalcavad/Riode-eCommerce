using MediatR;
using Riode.Data.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Business.Modules.UserModule
{
	public class UserEditCommand : IRequest<RiodeUser>
	{
		//public class UserEditCommandHandler : IRequestHandler<UserEditCommand, RiodeUser>
		//{
		//	public Task<RiodeUser> Handle(UserEditCommand request, CancellationToken cancellationToken)
		//	{
				
		//	}
		//}
	}
}
