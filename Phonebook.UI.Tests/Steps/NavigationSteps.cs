using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Phonebook.UI.Tests.Steps
{
	[Binding]
	public class NavigationSteps
	{
		[Given(@"I am on the Phonebook logon page")]
		public void GivenIAmOnThePhonebookLogonPage()
		{
			WebBrowser.Current.Navigate().GoToUrl("http://localhost/phonebook");
		}
	}
}
