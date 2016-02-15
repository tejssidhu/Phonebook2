using Phonebook.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phonebook.UI.Common;

namespace Phonebook.UI.Helpers
{
	public static class SessionExtensions
	{
		public static LogonModel GetLogonDetails(this HttpSessionStateBase session)
		{
			return session[SessionConstants.LogonDetails] as LogonModel;
		}

		public static UserDetailsModel GetUserDetails(this HttpSessionStateBase session)
		{
			return session[SessionConstants.UserDetails] as UserDetailsModel;
		}
	}
}