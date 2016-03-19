using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.UI.Tests.Drivers;
using Phonebook.UI.Tests.Interfaces;
using TechTalk.SpecFlow;

namespace Phonebook.UI.Tests.Steps
{
	[Binding]
	public class LogonFeatureSteps
	{
		private ILoginDriver LoginDriver { get; set; }

		//TODO: add DI ILoginDriver loginDriver
		public LogonFeatureSteps()
		{
			LoginDriver = new LoginDriver();
		}
		
		[When(@"I logon as a user with username: '(.*)' and password: '(.*)'")]
		public void WhenILogonAsAUserWithUsernameAndPassword(string username, string password)
		{
			LoginDriver.Logon(username, password);
		}

		[Then(@"The login page is displayed correctly")]
		public void ThenTheLoginPageIsDisplayedCorrectly()
		{
			var onLogonPage = LoginDriver.LoginPageIsShown();

			Assert.IsTrue(onLogonPage);
		}
		
		[Then(@"I am left on the logon page")]
		public void ThenIAmLeftOnTheLogonPage()
		{
			var onLogonPage = LoginDriver.LoginPageIsShown();

			Assert.IsTrue(onLogonPage);
		}

		[Then(@"A logon error message of ""(.*)"" is shown")]
		public void ThenALogonErrorMessageOfIsShown(string errorMessage)
		{
			Assert.AreEqual(errorMessage, LoginDriver.GetLogonErrorMessage());
		}

		[Then(@"The user's home page is displayed")]
		public void ThenTheUserSHomePageIsDisplayed()
		{
			var onHomePage = LoginDriver.WasLogonSuccessful();

			Assert.IsTrue(onHomePage);
		}
	}
}
