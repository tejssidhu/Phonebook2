using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Principal;

namespace Phonebook.WebApi.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class GenericAuthenticationFilter : AuthorizationFilterAttribute
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public GenericAuthenticationFilter()
		{
		
		}

		private readonly bool _isActive = true;

		/// <summary>
		/// parameter isActive explicitily enables/disables this filter
		/// </summary>
		/// <param name="isActive"></param>
		public GenericAuthenticationFilter(bool isActive)
		{
			_isActive = isActive;
		}

		/// <summary>
		/// Checks basic authentication request
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnAuthorization(HttpActionContext filterContext)
		{
			if (!_isActive) return;
			
			var identity = FetchAuthHeader(filterContext);

			if (identity == null)
			{
				ChallengeAuthRequest(filterContext);
				return;
			}

			var genericPrincipal = new GenericPrincipal(identity, null);

			Thread.CurrentPrincipal = genericPrincipal;
			HttpContext.Current.User = genericPrincipal;

			if (!OnAuthorizeUser(identity.Name, identity.Password, filterContext))
			{
				genericPrincipal = new GenericPrincipal(new GenericIdentity(""), new string[0]);

				Thread.CurrentPrincipal = genericPrincipal;
				HttpContext.Current.User = genericPrincipal;

				ChallengeAuthRequest(filterContext);

				return;
			}
			
			base.OnAuthorization(filterContext);
		}

		/// <summary>
		/// Virtual method.  Can be overriden with the custom Authorization
		/// </summary>
		/// <param name="user"></param>
		/// <param name="pass"></param>
		/// <param name="filterContext"></param>
		/// <returns></returns>
		protected virtual bool OnAuthorizeUser(string user, string pass, HttpActionContext filterContext)
		{
			if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
				return false;
			return true;
		}

		/// <summary>
		/// Checks for authorization header in the request and parses it, creates user credentials and returns as BasicAuthenticationIdentity
		/// </summary>
		/// <param name="filterContext"></param>
		/// <returns></returns>
		protected virtual BasicAuthenticationIdentity FetchAuthHeader(HttpActionContext filterContext) 
		{
			string authHeaderValue = null;
			var authRequest = filterContext.Request.Headers.Authorization;

			if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
				authHeaderValue = authRequest.Parameter;

			if (string.IsNullOrEmpty(authHeaderValue))
				return null;

			authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));

			var credentials = authHeaderValue.Split(':');

			return credentials.Length < 2 ? null : new BasicAuthenticationIdentity(credentials[0], credentials[1]);
		}

		private static void ChallengeAuthRequest(HttpActionContext filterContext)
		{
			var dnsHost = filterContext.Request.RequestUri.DnsSafeHost;

			filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
			//filterContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
		}
	}
}