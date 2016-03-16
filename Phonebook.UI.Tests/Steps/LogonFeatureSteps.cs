using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.UI.Tests.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Phonebook.UI.Tests.Steps
{
	[Binding]
	public class LogonFeatureSteps
	{
		//private ILoginDriver LoginDriver { get; set; }

		//public LogonFeatureSteps(ILoginDriver loginDriver)
		//{
		//	LoginDriver = loginDriver;
		//}

		[Given(@"I am on the Phonebook logon page")]
		public void GivenIAmOnThePhonebookLogonPage()
		{
			WebBrowser.Current.Navigate().GoToUrl("http://localhost/phonebook");
		}

		[When(@"I logon as a user with username: '(.*)' and password: '(.*)'")]
		public void WhenILogonAsAUserWithUsernameAndPassword(string username, string password)
		{
			var loginPage = new LoginDriver();

			loginPage.Logon(username, password);
		}

		[Then(@"The login page is displayed correctly")]
		public void ThenTheLoginPageIsDisplayedCorrectly()
		{
			ScenarioContext.Current.Pending();
		}
		
		[Then(@"I am left on the logon page")]
		public void ThenIAmLeftOnTheLogonPage()
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"A logon error message of ""(.*)"" is shown")]
		public void ThenALogonErrorMessageOfIsShown(string errorMessage)
		{
			var loginPage = new LoginDriver();

			Assert.AreEqual(errorMessage, loginPage.GetLogonErrorMessage());
		}

		[Then(@"The user's home page is displayed")]
		public void ThenTheUserSHomePageIsDisplayed()
		{
			ScenarioContext.Current.Pending();
		}


	}
}
