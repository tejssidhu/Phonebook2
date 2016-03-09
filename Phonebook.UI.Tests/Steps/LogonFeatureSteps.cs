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

		[Given(@"I am on the Phonebook logon page")]
		public void GivenIAmOnThePhonebookLogonPage()
		{
			//ScenarioContext.Current.Pending();
		}

		[When(@"I logon as a user with username: ""(.*)"" and password: ""(.*)""")]
		public void WhenILogonAsAUserWithUsernameAndPassword(string username, string password)
		{
			ScenarioContext.Current.Pending();
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
		public void ThenALogonErrorMessageOfIsShown(string p0)
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"A logon error icon is displayed for ""(.*)""")]
		public void ThenALogonErrorIconIsDisplayedFor(string p0)
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"The user's home page is displayed")]
		public void ThenTheUserSHomePageIsDisplayed()
		{
			ScenarioContext.Current.Pending();
		}


	}
}
