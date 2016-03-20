using Moq;
using Phonebook.WebApi.Tests.DependencyResolution;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Routing;

//http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/98d041ae352f#test/System.Web.Http.Test/Util/ContextUtil.cs

namespace Phonebook.WebApi.Tests.Utils
{
	internal static class ContextUtil
	{
		public static HttpControllerContext CreateControllerContext(HttpConfiguration configuration = null, IHttpController instance = null, IHttpRouteData routeData = null, HttpRequestMessage request = null)
		{
			HttpConfiguration config = configuration ?? new HttpConfiguration();
			IHttpRouteData route = routeData ?? new HttpRouteData(new HttpRoute());
			HttpRequestMessage req = request ?? new HttpRequestMessage();
			req.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
			req.Properties[HttpPropertyKeys.HttpRouteDataKey] = route;
			req.RequestUri = new System.Uri("http://myurl");

			HttpControllerContext context = new HttpControllerContext(config, route, req);
			if (instance != null)
			{
				context.Controller = instance;
			}
			context.ControllerDescriptor = CreateControllerDescriptor(config);

			return context;
		}

		public static HttpActionContext CreateActionContext(HttpControllerContext controllerContext = null, HttpActionDescriptor actionDescriptor = null)
		{
			HttpControllerContext context = controllerContext ?? ContextUtil.CreateControllerContext();
			HttpActionDescriptor descriptor = actionDescriptor ?? new Mock<HttpActionDescriptor>() { CallBase = true }.Object;
			return new HttpActionContext(context, descriptor);
		}

		public static HttpActionContext GetHttpActionContext(HttpRequestMessage request)
		{
			HttpActionContext actionContext = CreateActionContext();
			actionContext.ControllerContext.Request = request;
			return actionContext;
		}

		public static HttpActionExecutedContext GetActionExecutedContext(HttpRequestMessage request, HttpResponseMessage response)
		{
			HttpActionContext actionContext = CreateActionContext();
			actionContext.ControllerContext.Request = request;
			HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, null) { Response = response };
			return actionExecutedContext;
		}

		public static HttpControllerDescriptor CreateControllerDescriptor(HttpConfiguration config = null)
		{
			if (config == null)
			{
				config = new HttpConfiguration();
			}
			return new HttpControllerDescriptor();
		}

		public static HttpContextBase GetMockedHttpContext()
		{
			var context = new Mock<HttpContextBase>();
			var request = new Mock<HttpRequestBase>();
			var response = new Mock<HttpResponseBase>();
			var session = new Mock<HttpSessionStateBase>();
			var server = new Mock<HttpServerUtilityBase>();
			var user = new Mock<IPrincipal>();
			var identity = new Mock<IIdentity>();
			var urlHelper = new Mock<UrlHelper>();

			var routes = new RouteCollection();
			//MvcApplication.RegisterRoutes(routes);
			var requestContext = new Mock<RequestContext>();
			requestContext.Setup(x => x.HttpContext).Returns(context.Object);
			context.Setup(ctx => ctx.Request).Returns(request.Object);
			context.Setup(ctx => ctx.Response).Returns(response.Object);
			context.Setup(ctx => ctx.Session).Returns(session.Object);
			context.Setup(ctx => ctx.Server).Returns(server.Object);
			context.Setup(ctx => ctx.User).Returns(user.Object);
			user.Setup(ctx => ctx.Identity).Returns(identity.Object);
			identity.Setup(id => id.IsAuthenticated).Returns(false);
			identity.Setup(id => id.Name).Returns("");
			request.Setup(req => req.Url).Returns(new Uri("http://myurl"));
			request.Setup(req => req.RequestContext).Returns(requestContext.Object);
			requestContext.Setup(x => x.RouteData).Returns(new RouteData());
			request.SetupGet(req => req.Headers).Returns(new NameValueCollection());

			return context.Object;
		}
	}
}
