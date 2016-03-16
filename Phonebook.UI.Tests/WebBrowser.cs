using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Phonebook.UI.Tests
{
	public static class WebBrowser
	{
		private const string Key = "browser";

		public static IWebDriver Current
		{
			get
			{
				if (!ScenarioContext.Current.ContainsKey(Key))
				{
					var driver = new FirefoxDriver();

					ScenarioContext.Current[Key] = driver;
				}

				return ScenarioContext.Current[Key] as IWebDriver;
			}
		}
	}
}
