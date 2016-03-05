using Phonebook.Domain.Interfaces.Services;
using System.Threading;
using System.Web.Http.Controllers;
using StructureMap;
using System;

namespace Phonebook.WebApi.Filters
{
	public class ApiAuthenticationFilter : GenericAuthenticationFilter
	{

		public ApiAuthenticationFilter()
		{

		}

		public ApiAuthenticationFilter(bool isActive) : base(isActive)
		{

		}

		protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
		{
			var userService = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;
			
			if (userService != null)
			{
				Guid userId = Guid.Empty;

				try
				{
					userId = userService.Authenticate(username, password).Id;
				}
				catch 
				{
					return false;
				}

				if (userId != Guid.Empty)
				{
					var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
					if (basicAuthenticationIdentity != null)
					{ 
						basicAuthenticationIdentity.UserId = userId;
					}

					return true;
				}
			}

			return false;
		}
	}
}