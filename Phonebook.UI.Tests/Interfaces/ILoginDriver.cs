using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.UI.Tests.Interfaces
{
	public interface ILoginDriver
	{
		bool LoginPageIsShown();
		void Logon(string username, string password);
		string GetLogonErrorMessage();
		bool WasLogonSuccessful();
		bool Logout();
	}
}
