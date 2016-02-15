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

namespace Phonebook.WebApi.Controllers
{
	[ApiAuthenticationFilter]
    public class LogonController : ApiController
    {
		private readonly IUserService _userService;

		public LogonController(IUserService userService)
        {
            _userService = userService;
        }

		public IHttpActionResult Logon(LogonModel model)
		{
			return Ok(Thread.CurrentPrincipal.Identity);
		}
		
		public void Dispose()
		{
			_userService.Dispose();
		}
	}
}
