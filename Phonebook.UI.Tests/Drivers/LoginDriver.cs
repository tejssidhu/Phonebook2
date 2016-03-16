using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Phonebook.UI.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.UI.Tests.Drivers
{
	public class LoginDriver : ILoginDriver
	{
		public bool LoginPageIsShown()
		{
			return false;
		}

		public void Logon(string username, string password)
		{
			IWebElement usernameField = WebBrowser.Current.FindElement(By.Id("UserName"));
			usernameField.SendKeys(username);

			IWebElement passwordField = WebBrowser.Current.FindElement(By.Id("Password"));
			passwordField.SendKeys(password);

			passwordField.Submit();
		}

		public string GetLogonErrorMessage()
		{
			IWebElement errorField = WebBrowser.Current.FindElement(By.Id("error"));

			return errorField.Text;
		}

		public bool WasLogonSuccessful()
		{
			throw new NotImplementedException();
		}

		public bool Logout()
		{
			throw new NotImplementedException();
		}
	}
}
