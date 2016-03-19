using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
			WebBrowser.WaitForAjax();

			IWebElement usernameField = WebBrowser.FindElement(By.Id("UserName"));
			IWebElement passwordField = WebBrowser.FindElement(By.Id("Password"));

			if (usernameField == null || passwordField == null)
				return false;

			return true;
		}

		public void Logon(string username, string password)
		{
			//WebBrowser.WaitUntilPageLoaded();
			WebBrowser.WaitForAjax();

			IWebElement usernameField = WebBrowser.FindElement(By.Id("UserName"));
			usernameField.SendKeys(username);

			IWebElement passwordField = WebBrowser.FindElement(By.Id("Password"));
			passwordField.SendKeys(password);

			passwordField.Submit();
		}

		public string GetLogonErrorMessage()
		{
			WebBrowser.WaitForAjax();

			IWebElement errorField = WebBrowser.FindElement(By.Id("error"));

			return errorField.Text;
		}

		public bool WasLogonSuccessful()
		{
			WebBrowser.WaitForAjax();
			IWebElement navBarBrand = WebBrowser.FindElement(By.ClassName("navbar-brand"));

			if (navBarBrand == null)
				return false;

			return true;
		}

		public bool Logout()
		{
			throw new NotImplementedException();
		}
	}
}
