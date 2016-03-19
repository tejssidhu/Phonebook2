using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Phonebook.UI.Tests
{
	public static class WebBrowser
	{
		private const string Key = "browser";
		private const double MaxTimeToWait = 30.00;

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

		public static void WaitUntilPageLoaded()
		{
			IWait<IWebDriver> wait = new WebDriverWait(Current, TimeSpan.FromSeconds(MaxTimeToWait));
			wait.Until(driver1 => ((IJavaScriptExecutor)Current).ExecuteScript("return document.readyState").Equals("complete"));
		}

		public static void WaitForAjax()
		{
			while (true) // Handle timeout somewhere
			{
				var ajaxIsComplete = (bool)(Current as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
				if (ajaxIsComplete)
					break;
				Thread.Sleep(100);
			}
		}

		public static IWebElement FindElement(By findBy)
		{
			IWait<IWebDriver> wait = new WebDriverWait(Current, TimeSpan.FromSeconds(MaxTimeToWait));
			wait.Until(ExpectedConditions.ElementIsVisible(findBy));

			return Current.FindElement(findBy);
		}
	}
}
