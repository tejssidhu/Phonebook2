using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Controllers;
using Phonebook.WebApi.Filters;
using Phonebook.WebApi.Tests.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;

namespace Phonebook.WebApi.Tests
{
	[TestClass]
	public class LogonTests
	{

		#region Test Initialise and Cleanup

		private List<User> _users;
		private User _user;

		[TestInitialize]
		public void TestUserServiceTests()
		{
			_user = new User { Id = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"), Password = "789", Username = "User789" };

			var user1 = new User { Id = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"), Password = "123", Username = "User123" };
			var user2 = new User { Id = new Guid("5875412f-e8b8-493e-bd58-5df35083342c"), Password = "456", Username = "User456" };
			var user4 = new User { Id = new Guid("cef70a7a-3349-4368-85ed-66b8c274fad1"), Password = "p6NY0hg", Username = "mjenkins0" };
			var user5 = new User { Id = new Guid("71d8e924-7c58-4424-9e1b-b14eefa76abc"), Password = "5w7JhI42GLC", Username = "amartin1" };
			var user6 = new User { Id = new Guid("a051d1ca-a3c5-45d4-be60-5bc5256ce83e"), Password = "3NypkQZSe", Username = "vallen2" };
			var user7 = new User { Id = new Guid("2b3b4d72-1c15-40e0-a05a-012b724950c3"), Password = "MsNDnRy1", Username = "mblack3" };
			var user8 = new User { Id = new Guid("2550f510-e5c9-45a4-90a0-c286e4bcd948"), Password = "8dpEdKRn", Username = "schapman4" };
			var user9 = new User { Id = new Guid("874c0bc3-6d9b-4dfa-b42c-8403fe1b281d"), Password = "7s7G9nai", Username = "gdiaz5" };
			var user10 = new User { Id = new Guid("16c6e264-0091-45f6-b9fd-02716d8d62dd"), Password = "3h7Vnh9rUpCl", Username = "cwheeler6" };
			var user11 = new User { Id = new Guid("0d1a6711-e9eb-418e-adda-47a62a7900c9"), Password = "g8KhtQpk", Username = "bparker7" };

			_users = new List<User> { user1, user2, _user, user4, user5, user6, user7, user8, user9, user10, user11 };
		}

		#endregion

		[TestMethod]
		public void AuthenticateWithValidPassword()
		{
			//arrange
			Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new string[0]);
			var mockService = new Mock<IUserService>();
			var user = new User
			{
				Id = Guid.NewGuid()
			};
			mockService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

			HttpConfiguration config = new HttpConfiguration();
			var container = new Container();
			container.Configure(x => x.For<IUserService>().Use(mockService.Object));
			config.DependencyResolver = new StructureMapResolver(container);

			var controllerControllerContext = Utils.ContextUtil.CreateControllerContext(config);
			var actionContext = Utils.ContextUtil.CreateActionContext(controllerControllerContext);
						
			actionContext.Request.Headers.Add("Authorization", "Basic " + Base64Encode("User1:Pass1"));
			var apiAuthenticationFilter = new ApiAuthenticationFilter(true);
			
			//act
			apiAuthenticationFilter.OnAuthorization(actionContext);

			//assert
			var identity = Thread.CurrentPrincipal.Identity;
			var basicIdentify = (BasicAuthenticationIdentity)identity;
			Assert.IsTrue(identity.IsAuthenticated);
			Assert.AreEqual(identity.Name, "User1");
			Assert.AreEqual(basicIdentify.UserId, user.Id);
		}

		[TestMethod]
		public void AuthenticateWithInvalidPassword()
		{
			//arrange
			Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new string[0]);
			var mockService = new Mock<IUserService>();
			var user = new User
			{
				Id = Guid.NewGuid()
			};
			mockService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Throws(new InvalidPasswordException());

			HttpConfiguration config = new HttpConfiguration();
			var container = new Container();
			container.Configure(x => x.For<IUserService>().Use(mockService.Object));
			config.DependencyResolver = new StructureMapResolver(container);

			var controllerControllerContext = Utils.ContextUtil.CreateControllerContext(config);
			var actionContext = Utils.ContextUtil.CreateActionContext(controllerControllerContext);

			actionContext.Request.Headers.Add("Authorization", "Basic " + Base64Encode("User2:Pass2"));
			var apiAuthenticationFilter = new ApiAuthenticationFilter(true);

			//act
			apiAuthenticationFilter.OnAuthorization(actionContext);

			//assert
			var identity = Thread.CurrentPrincipal.Identity;
			Assert.IsFalse(identity.IsAuthenticated);
			Assert.AreEqual(identity.Name, "");
			Assert.AreEqual(actionContext.Response.StatusCode, HttpStatusCode.Unauthorized);
		}

		[TestMethod]
		public void AuthenticateWithInvalidUsername()
		{
			//arrange
			Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new string[0]);
			var mockService = new Mock<IUserService>();
			var user = new User
			{
				Id = Guid.NewGuid()
			};
			mockService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Throws(new ObjectNotFoundException("User"));

			HttpConfiguration config = new HttpConfiguration();
			var container = new Container();
			container.Configure(x => x.For<IUserService>().Use(mockService.Object));
			config.DependencyResolver = new StructureMapResolver(container);

			var controllerControllerContext = Utils.ContextUtil.CreateControllerContext(config);
			var actionContext = Utils.ContextUtil.CreateActionContext(controllerControllerContext);

			actionContext.Request.Headers.Add("Authorization", "Basic " + Base64Encode("User2:Pass2"));
			var apiAuthenticationFilter = new ApiAuthenticationFilter(true);

			//act
			apiAuthenticationFilter.OnAuthorization(actionContext);

			//assert
			var identity = Thread.CurrentPrincipal.Identity;
			Assert.IsFalse(identity.IsAuthenticated);
			Assert.AreEqual(identity.Name, "");
			Assert.AreEqual(actionContext.Response.StatusCode, HttpStatusCode.Unauthorized);
		}

		private static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
	}
}
