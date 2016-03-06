using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Models;
using Phonebook.WebApi.Filters;
using System.Threading;
using System.Configuration;

namespace Phonebook.WebApi.Controllers
{
	
	public class LogonController : ApiController
	{
		private readonly ITokenService _tokenService;

		public LogonController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		[ApiAuthenticationFilter]
		public HttpResponseMessage Logon(LogonModel model)
		{
			if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
			{
				var basicAuthId = System.Web.HttpContext.Current.User.Identity as BasicAuthenticationIdentity;
				if (basicAuthId != null)
				{
					var userId = basicAuthId.UserId;
					return GetAuthToken(userId);
				}
			}

			return new HttpResponseMessage(HttpStatusCode.Unauthorized);
		}

		private HttpResponseMessage GetAuthToken(Guid userId)
		{
			var token = _tokenService.GenerateToken(userId);

			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Headers.Add("Token", token.AuthToken);
			response.Headers.Add("UserId", userId.ToString());
			response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
			response.Headers.Add("Access-Control-Expose-Headers", "Token,UserId,TokenExpiry");

			return response;
		}
		
		public void Dispose()
		{
			_tokenService.Dispose();
		}
	}
}
