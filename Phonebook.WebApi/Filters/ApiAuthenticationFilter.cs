using Phonebook.Domain.Interfaces.Services;
using System.Threading;
using System.Web.Http.Controllers;

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
			var provider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;

			if (provider != null)
			{
				var userId = provider.Authenticate(username, password).Id;
				if (userId != null)
				{
					var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
					if (basicAuthenticationIdentity != null)
						basicAuthenticationIdentity.UserId = userId;
					return true;
				}
			}
			return false;
		}
	}
}