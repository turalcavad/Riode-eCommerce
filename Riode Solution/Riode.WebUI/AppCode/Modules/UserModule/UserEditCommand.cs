using MediatR;
using Riode.WebUI.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.WebUI.AppCode.Modules.UserModule
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
